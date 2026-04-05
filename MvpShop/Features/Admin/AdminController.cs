using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvpShop.Data;

namespace MvpShop.Features.Admin;

public class AdminController(
    AppDbContext dbContext,
    ILogger<AdminController> logger) : Controller
{
    [HttpGet("admin")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = await dbContext.Products
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
        logger.LogInformation("Admin page opened, {ProductsCount} products loaded.", products.Count);

        return View(products);
    }
}
