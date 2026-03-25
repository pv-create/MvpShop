using MvpShop.Features.Cart;

namespace MvpShop.Features.Orders;

public class CheckoutViewModel
{
    public CheckoutInputModel Input { get; init; } = new();

    public IReadOnlyList<CartItem> Items { get; init; } = [];

    public decimal TotalAmount => Items.Sum(x => x.Price * x.Quantity);

    public int TotalQuantity => Items.Sum(x => x.Quantity);
}
