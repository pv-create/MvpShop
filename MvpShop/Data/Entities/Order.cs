using System.ComponentModel.DataAnnotations;

namespace MvpShop.Data.Entities;

public class Order
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(50)]
    public string CustomerPhone { get; set; } = string.Empty;

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItem> Items { get; set; } = [];
}
