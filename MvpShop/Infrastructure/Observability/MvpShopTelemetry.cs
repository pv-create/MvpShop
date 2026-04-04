using System.Diagnostics;

namespace MvpShop.Infrastructure.Observability;

public static class MvpShopTelemetry
{
    public const string ServiceName = "MvpShop";
    public const string ActivitySourceName = "MvpShop";

    public static readonly ActivitySource ActivitySource = new(ActivitySourceName);
}
