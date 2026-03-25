using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvpShop.Data;

namespace MvpShop.Features.Cart;

public class CartController(AppDbContext dbContext, CartService cartService) : Controller
{
    [HttpGet("cart")]
    public IActionResult Index()
    {
        return View(new CartViewModel
        {
            Items = cartService.GetCartItems()
        });
    }

    [HttpPost("cart/add-item")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(int productId, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        cartService.AddToCart(new CartItem
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = 1
        });

        return Json(new
        {
            totalQuantity = cartService.GetTotalQuantity()
        });
    }

    [HttpPost("cart/update-quantity")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        cartService.UpdateQuantity(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("cart/remove-item")]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveItem(int productId)
    {
        cartService.RemoveFromCart(productId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("cart/clear")]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        cartService.ClearCart();
        return RedirectToAction(nameof(Index));
    }
}
