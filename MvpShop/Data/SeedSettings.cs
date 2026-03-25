namespace MvpShop.Data;

public class SeedSettings
{
    public const string SectionName = "Seed";

    public bool ApplyMigrationsOnStartup { get; set; } = true;

    public bool ForceReseedProducts { get; set; }
}
