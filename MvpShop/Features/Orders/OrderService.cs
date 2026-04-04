using Microsoft.EntityFrameworkCore;
using MvpShop.Data;
using MvpShop.Data.Entities;
using MvpShop.Features.Cart;
using MvpShop.Infrastructure.Observability;
using MvpShop.Infrastructure.Telegram;
using Prometheus;

namespace MvpShop.Features.Orders;

public class OrderService(
    AppDbContext dbContext,
    ITelegramService telegramService,
    ILogger<OrderService> logger)
{
    private static readonly Counter OrdersCreatedCounter = Metrics
        .CreateCounter("mvpshop_orders_created_total", "Total number of created orders.");

    public async Task<Order> CreateOrderAsync(
        CheckoutInputModel input,
        IReadOnlyList<CartItem> cartItems,
        CancellationToken cancellationToken)
    {
        if (cartItems.Count == 0)
        {
            throw new InvalidOperationException("Cannot create order from an empty cart.");
        }

        using var activity = MvpShopTelemetry.ActivitySource.StartActivity("orders.create");
        activity?.SetTag("order.items_count", cartItems.Count);
        activity?.SetTag("order.total_amount", cartItems.Sum(x => x.Price * x.Quantity));

        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        Order? order = null;

        await executionStrategy.ExecuteAsync(async () =>
        {
            using var persistenceActivity = MvpShopTelemetry.ActivitySource.StartActivity("orders.persist");
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

            order = new Order
            {
                CustomerName = input.CustomerName.Trim(),
                CustomerEmail = input.CustomerEmail.Trim(),
                CustomerPhone = input.CustomerPhone.Trim(),
                OrderDate = DateTimeOffset.UtcNow,
                TotalAmount = cartItems.Sum(x => x.Price * x.Quantity),
                Status = OrderStatus.Pending,
                Items = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.Name,
                    UnitPrice = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };

            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });

        if (order is null)
        {
            throw new InvalidOperationException("Order creation failed.");
        }

        activity?.SetTag("order.id", order.Id);
        OrdersCreatedCounter.Inc();

        try
        {
            await telegramService.SendOrderNotificationAsync(order, order.Items, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Telegram notification failed for order {OrderId}.", order.Id);
            activity?.SetStatus(System.Diagnostics.ActivityStatusCode.Error, exception.Message);
        }

        return order;
    }

    public async Task<Order?> GetOrderAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
