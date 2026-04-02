using Microsoft.EntityFrameworkCore;
using MvpShop.Data;
using MvpShop.Data.Entities;
using MvpShop.Features.Cart;
using MvpShop.Infrastructure.Telegram;

namespace MvpShop.Features.Orders;

public class OrderService(
    AppDbContext dbContext,
    ITelegramService telegramService,
    ILogger<OrderService> logger)
{
    public async Task<Order> CreateOrderAsync(
        CheckoutInputModel input,
        IReadOnlyList<CartItem> cartItems,
        CancellationToken cancellationToken)
    {
        if (cartItems.Count == 0)
        {
            throw new InvalidOperationException("Cannot create order from an empty cart.");
        }

        var executionStrategy = dbContext.Database.CreateExecutionStrategy();
        Order? order = null;

        await executionStrategy.ExecuteAsync(async () =>
        {
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

        try
        {
            await telegramService.SendOrderNotificationAsync(order, order.Items, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Telegram notification failed for order {OrderId}.", order.Id);
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
