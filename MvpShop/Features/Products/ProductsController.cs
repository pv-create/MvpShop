using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvpShop.Data;
using MvpShop.Data.Entities;

namespace MvpShop.Features.Products;

public class ProductsController(AppDbContext dbContext) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return View(products);
    }

    [HttpGet("products/{id:int}")]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpGet("products/create")]
    public IActionResult Create()
    {
        return View(new ProductFormModel());
    }

    [HttpPost("products/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var product = new Product
        {
            Name = model.Name.Trim(),
            Description = model.Description.Trim(),
            Price = model.Price,
            ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? null : model.ImageUrl.Trim()
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Details), new { id = product.Id });
    }

    [HttpGet("products/{id:int}/edit")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return View(new ProductFormModel
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        });
    }

    [HttpPost("products/{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductFormModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        product.Name = model.Name.Trim();
        product.Description = model.Description.Trim();
        product.Price = model.Price;
        product.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? null : model.ImageUrl.Trim();

        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(Details), new { id = product.Id });
    }

    [HttpGet("products/{id:int}/delete")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    [HttpPost("products/{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);

        return RedirectToAction(nameof(List));
    }
}
