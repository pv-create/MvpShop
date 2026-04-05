using Microsoft.AspNetCore.Mvc;
using MvpShop.Features.Cart;

namespace MvpShop.Features.Orders;

public class OrdersController(
    CartService cartService,
    OrderService orderService,
    ILogger<OrdersController> logger) : Controller
{
    [HttpGet("orders/checkout")]
    public IActionResult Checkout()
    {
        var items = cartService.GetCartItems();

        if (items.Count == 0)
        {
            logger.LogDebug("Checkout page requested with empty cart, redirecting to cart.");
            return RedirectToAction("Index", "Cart");
        }

        logger.LogDebug("Checkout page opened with {ItemsCount} positions.", items.Count);
        return View(new CheckoutViewModel
        {
            Items = items
        });
    }

    [HttpPost("orders/checkout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout([Bind(Prefix = "Input")] CheckoutInputModel input, CancellationToken cancellationToken)
    {
        var items = cartService.GetCartItems();

        if (items.Count == 0)
        {
            logger.LogWarning("Checkout submission rejected because cart is empty.");
            return RedirectToAction("Index", "Cart");
        }

        if (!ModelState.IsValid)
        {
            logger.LogDebug("Checkout form validation failed.");
            return View(new CheckoutViewModel
            {
                Input = input,
                Items = items
            });
        }

        var order = await orderService.CreateOrderAsync(input, items, cancellationToken);
        cartService.ClearCart();
        logger.LogInformation("Checkout completed, redirecting to confirmation for order {OrderId}.", order.Id);

        return RedirectToAction(nameof(Confirmation), new { orderId = order.Id });
    }

    [HttpGet("orders/confirmation/{orderId:int}")]
    public async Task<IActionResult> Confirmation(int orderId, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderAsync(orderId, cancellationToken);

        if (order is null)
        {
            logger.LogWarning("Confirmation page requested for missing order {OrderId}.", orderId);
            return NotFound();
        }

        logger.LogDebug("Confirmation page opened for order {OrderId}.", orderId);
        return View(new ConfirmationViewModel
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName
        });
    }
}
