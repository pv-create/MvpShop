using Microsoft.AspNetCore.Mvc;
using MvpShop.Features.Cart;

namespace MvpShop.Features.Orders;

public class OrdersController(CartService cartService, OrderService orderService) : Controller
{
    [HttpGet("orders/checkout")]
    public IActionResult Checkout()
    {
        var items = cartService.GetCartItems();

        if (items.Count == 0)
        {
            return RedirectToAction("Index", "Cart");
        }

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
            return RedirectToAction("Index", "Cart");
        }

        if (!ModelState.IsValid)
        {
            return View(new CheckoutViewModel
            {
                Input = input,
                Items = items
            });
        }

        var order = await orderService.CreateOrderAsync(input, items, cancellationToken);
        cartService.ClearCart();

        return RedirectToAction(nameof(Confirmation), new { orderId = order.Id });
    }

    [HttpGet("orders/confirmation/{orderId:int}")]
    public async Task<IActionResult> Confirmation(int orderId, CancellationToken cancellationToken)
    {
        var order = await orderService.GetOrderAsync(orderId, cancellationToken);

        if (order is null)
        {
            return NotFound();
        }

        return View(new ConfirmationViewModel
        {
            OrderId = order.Id,
            CustomerName = order.CustomerName
        });
    }
}
