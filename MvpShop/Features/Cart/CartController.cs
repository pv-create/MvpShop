using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvpShop.Data;

namespace MvpShop.Features.Cart;

public class CartController(
    AppDbContext dbContext,
    CartService cartService,
    ILogger<CartController> logger) : Controller
{
    [HttpGet("cart")]
    public IActionResult Index()
    {
        var items = cartService.GetCartItems();
        logger.LogDebug(
            "Cart page opened with {UniqueItemsCount} positions and total quantity {TotalQuantity}.",
            items.Count,
            items.Sum(x => x.Quantity));

        return View(new CartViewModel
        {
            Items = items
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
            logger.LogWarning("Add to cart failed because product {ProductId} was not found.", productId);
            return NotFound();
        }

        cartService.AddToCart(new CartItem
        {
            ProductId = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = 1
        });
        logger.LogDebug("Product {ProductId} added to cart from request.", product.Id);

        if (string.Equals(Request.Headers.XRequestedWith, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase))
        {
            return Json(new
            {
                totalQuantity = cartService.GetTotalQuantity()
            });
        }

        var referer = Request.Headers.Referer.ToString();
        if (Uri.TryCreate(referer, UriKind.Absolute, out var refererUri))
        {
            return Redirect(refererUri.PathAndQuery);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("cart/update-quantity")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateQuantity(int productId, int quantity)
    {
        cartService.UpdateQuantity(productId, quantity);
        logger.LogDebug("Cart quantity update requested for product {ProductId}: {Quantity}.", productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("cart/remove-item")]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveItem(int productId)
    {
        cartService.RemoveFromCart(productId);
        logger.LogDebug("Cart item removal requested for product {ProductId}.", productId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("cart/clear")]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        cartService.ClearCart();
        logger.LogDebug("Cart clear requested.");
        return RedirectToAction(nameof(Index));
    }
}
