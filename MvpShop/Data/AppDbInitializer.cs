using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MvpShop.Data;

public static class AppDbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var seedSettings = scope.ServiceProvider.GetRequiredService<IOptions<SeedSettings>>().Value;

        if (seedSettings.ApplyMigrationsOnStartup)
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        var hasProducts = await dbContext.Products.AnyAsync(cancellationToken);

        if (!seedSettings.ForceReseedProducts && hasProducts)
        {
            return;
        }

        if (seedSettings.ForceReseedProducts && hasProducts)
        {
            dbContext.Products.RemoveRange(dbContext.Products);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        dbContext.Products.AddRange(ProductSeed.MongolianProducts);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
