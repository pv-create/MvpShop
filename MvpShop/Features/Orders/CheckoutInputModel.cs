using System.ComponentModel.DataAnnotations;

namespace MvpShop.Features.Orders;

public class CheckoutInputModel
{
    [Required(ErrorMessage = "Укажите имя.")]
    [StringLength(200)]
    [Display(Name = "Имя")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите email.")]
    [EmailAddress(ErrorMessage = "Укажите корректный email.")]
    [StringLength(320)]
    [Display(Name = "Email")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите телефон.")]
    [Phone(ErrorMessage = "Укажите корректный телефон.")]
    [StringLength(50)]
    [Display(Name = "Телефон")]
    public string CustomerPhone { get; set; } = string.Empty;
}
