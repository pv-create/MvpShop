using System.ComponentModel.DataAnnotations;

namespace MvpShop.Features.Products;

public class ProductFormModel
{
    [Required(ErrorMessage = "Укажите название товара.")]
    [StringLength(200)]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    [StringLength(4000)]
    [Display(Name = "Описание")]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0", "999999999", ErrorMessage = "Цена должна быть неотрицательной.")]
    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    [Url(ErrorMessage = "Укажите корректный URL изображения.")]
    [StringLength(1000)]
    [Display(Name = "URL изображения")]
    public string? ImageUrl { get; set; }
}
