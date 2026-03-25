namespace MvpShop.Features.Orders;

public class ConfirmationViewModel
{
    public int OrderId { get; init; }

    public string CustomerName { get; init; } = string.Empty;
}
