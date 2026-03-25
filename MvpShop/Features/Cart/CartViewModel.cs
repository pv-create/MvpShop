namespace MvpShop.Features.Cart;

public class CartViewModel
{
    public IReadOnlyList<CartItem> Items { get; init; } = [];

    public decimal TotalAmount => Items.Sum(x => x.Price * x.Quantity);

    public int TotalQuantity => Items.Sum(x => x.Quantity);
}
