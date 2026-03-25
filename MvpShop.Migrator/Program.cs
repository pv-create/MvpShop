using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MvpShop.Data;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var connectionString = configuration["ConnectionStrings:DefaultConnection"]
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

var seedSettings = configuration
    .GetSection(SeedSettings.SectionName)
    .Get<SeedSettings>() ?? new SeedSettings();

var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    })
    .Options;

const int maxAttempts = 10;

for (var attempt = 1; attempt <= maxAttempts; attempt++)
{
    try
    {
        Console.WriteLine($"Applying migrations. Attempt {attempt} of {maxAttempts}.");

        await using var dbContext = new AppDbContext(options);
        await dbContext.Database.MigrateAsync();
        await SeedProductsAsync(dbContext, seedSettings);

        Console.WriteLine("Migrations applied successfully.");
        return;
    }
    catch (Exception exception) when (attempt < maxAttempts)
    {
        Console.WriteLine($"Migration attempt {attempt} failed: {exception.Message}");
        await Task.Delay(TimeSpan.FromSeconds(5));
    }
}

throw new InvalidOperationException("Failed to apply migrations after multiple attempts.");

static async Task SeedProductsAsync(AppDbContext dbContext, SeedSettings seedSettings)
{
    var hasProducts = await dbContext.Products.AnyAsync();

    if (!seedSettings.ForceReseedProducts && hasProducts)
    {
        return;
    }

    if (seedSettings.ForceReseedProducts && hasProducts)
    {
        dbContext.Products.RemoveRange(dbContext.Products);
        await dbContext.SaveChangesAsync();
    }

    dbContext.Products.AddRange(ProductSeed.MongolianProducts);
    await dbContext.SaveChangesAsync();

    Console.WriteLine($"Seeded {ProductSeed.MongolianProducts.Count} Mongolian products.");
}
