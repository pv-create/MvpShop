using Microsoft.EntityFrameworkCore;
using MvpShop.Data;
using MvpShop.Features.Orders;
using MvpShop.Infrastructure.Localization;
using MvpShop.Infrastructure.Observability;
using MvpShop.Infrastructure.Telegram;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SeedSettings>(
    builder.Configuration.GetSection(SeedSettings.SectionName));

builder.Services.Configure<TelegramSettings>(
    builder.Configuration.GetSection(TelegramSettings.SectionName));

builder.Services.AddDataProtection();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AppLocalizer>();
builder.Services.AddScoped<MvpShop.Features.Cart.CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddSingleton<DatabaseRecoveryService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)));

var otlpEndpoint = builder.Configuration["OpenTelemetry:Otlp:Endpoint"];
if (!string.IsNullOrWhiteSpace(otlpEndpoint))
{
    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService(MvpShopTelemetry.ServiceName))
        .WithTracing(tracing => tracing
            .AddSource(MvpShopTelemetry.ActivitySourceName)
            .AddAspNetCoreInstrumentation(options => options.RecordException = true)
            .AddHttpClientInstrumentation(options => options.RecordException = true)
            .AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri(otlpEndpoint);
            }));
}

builder.Services
    .AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
        options.ViewLocationFormats.Add("/Shared/{0}.cshtml");
    });

var app = builder.Build();

await app.Services.GetRequiredService<DatabaseRecoveryService>().EnsureInitializedAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseHttpMetrics();
app.UseMiddleware<DatabaseRecoveryMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapMetrics();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

if (!IsEfTooling())
{
    app.Run();
}

static bool IsEfTooling()
{
    return Environment.CommandLine.Contains("ef.dll", StringComparison.OrdinalIgnoreCase);
}

public partial class Program;
