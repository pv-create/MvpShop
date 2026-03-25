namespace MvpShop.Infrastructure.Telegram;

public class TelegramSettings
{
    public const string SectionName = "Telegram";

    public string BotToken { get; set; } = string.Empty;

    public string ChatId { get; set; } = string.Empty;
}
