using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvpShop.Data;
using MvpShop.Data.Entities;

namespace MvpShop.Features.Products;

public class ProductsController(
    AppDbContext dbContext,
    ILogger<ProductsController> logger) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
        logger.LogDebug("Catalog page opened, {ProductsCount} products loaded.", products.Count);

        return View(products);
    }

    [HttpGet("products/{id:int}")]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product details requested for missing product {ProductId}.", id);
            return NotFound();
        }

        logger.LogDebug("Product details opened for product {ProductId}.", id);
        return View(product);
    }

    [HttpGet("products/create")]
    public IActionResult Create()
    {
        logger.LogDebug("Product create page opened.");
        return View(new ProductFormModel());
    }

    [HttpPost("products/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductFormModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            logger.LogDebug("Product create validation failed.");
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
        logger.LogInformation("Product {ProductId} created.", product.Id);

        return RedirectToAction(nameof(Details), new { id = product.Id });
    }

    [HttpGet("products/{id:int}/edit")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product edit requested for missing product {ProductId}.", id);
            return NotFound();
        }

        logger.LogDebug("Product edit page opened for product {ProductId}.", id);
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
            logger.LogDebug("Product edit validation failed for product {ProductId}.", id);
            return View(model);
        }

        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product edit submission failed because product {ProductId} was not found.", id);
            return NotFound();
        }

        product.Name = model.Name.Trim();
        product.Description = model.Description.Trim();
        product.Price = model.Price;
        product.ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? null : model.ImageUrl.Trim();

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product {ProductId} updated.", product.Id);

        return RedirectToAction(nameof(Details), new { id = product.Id });
    }

    [HttpGet("products/{id:int}/delete")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product is null)
        {
            logger.LogWarning("Product delete page requested for missing product {ProductId}.", id);
            return NotFound();
        }

        logger.LogDebug("Product delete page opened for product {ProductId}.", id);
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
            logger.LogWarning("Product delete submission failed because product {ProductId} was not found.", id);
            return NotFound();
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product {ProductId} deleted.", id);

        return RedirectToAction(nameof(List));
    }
}
