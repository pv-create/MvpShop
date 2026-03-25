using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

namespace MvpShop.Features.Cart;

public class CartService(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
{
    private const string CartCookieName = "mvp_shop_cart";
    private readonly IDataProtector _protector = dataProtectionProvider.CreateProtector("MvpShop.Cart.Cookie");

    public void AddToCart(CartItem item)
    {
        var items = GetCartItems();
        var existingItem = items.FirstOrDefault(x => x.ProductId == item.ProductId);

        if (existingItem is null)
        {
            items.Add(item);
        }
        else
        {
            existingItem.Quantity += item.Quantity;
        }

        SaveCart(items);
    }

    public void RemoveFromCart(int productId)
    {
        var items = GetCartItems();
        items.RemoveAll(x => x.ProductId == productId);
        SaveCart(items);
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var items = GetCartItems();
        var item = items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
        {
            return;
        }

        if (quantity <= 0)
        {
            items.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        SaveCart(items);
    }

    public List<CartItem> GetCartItems()
    {
        var context = GetHttpContext();
        var protectedCart = context.Request.Cookies[CartCookieName];

        if (string.IsNullOrWhiteSpace(protectedCart))
        {
            return [];
        }

        try
        {
            var json = _protector.Unprotect(protectedCart);
            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? [];
        }
        catch
        {
            ClearCart();
            return [];
        }
    }

    public void ClearCart()
    {
        var context = GetHttpContext();
        context.Response.Cookies.Delete(CartCookieName);
    }

    public int GetTotalQuantity()
    {
        return GetCartItems().Sum(x => x.Quantity);
    }

    private void SaveCart(List<CartItem> items)
    {
        var context = GetHttpContext();

        if (items.Count == 0)
        {
            context.Response.Cookies.Delete(CartCookieName);
            return;
        }

        var json = JsonSerializer.Serialize(items);
        var protectedCart = _protector.Protect(json);

        context.Response.Cookies.Append(CartCookieName, protectedCart, new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Secure = context.Request.IsHttps,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(14)
        });
    }

    private HttpContext GetHttpContext()
    {
        return httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HTTP context is not available.");
    }
}
