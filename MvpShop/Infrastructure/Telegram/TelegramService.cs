using System.Globalization;
using System.Net;
using Microsoft.Extensions.Options;
using MvpShop.Data.Entities;
using MvpShop.Infrastructure.Observability;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace MvpShop.Infrastructure.Telegram;

public class TelegramService(
    IOptions<TelegramSettings> settings,
    ILogger<TelegramService> logger) : ITelegramService
{
    public async Task SendOrderNotificationAsync(Order order, List<OrderItem> items, CancellationToken cancellationToken = default)
    {
        var options = settings.Value;
        using var activity = MvpShopTelemetry.ActivitySource.StartActivity("telegram.send_order_notification");
        activity?.SetTag("messaging.system", "telegram");
        activity?.SetTag("order.id", order.Id);
        activity?.SetTag("telegram.chat_id", options.ChatId);

        if (string.IsNullOrWhiteSpace(options.BotToken) || string.IsNullOrWhiteSpace(options.ChatId))
        {
            logger.LogInformation("Telegram notification skipped because bot settings are not configured.");
            return;
        }

        var botClient = new TelegramBotClient(options.BotToken);
        var message = BuildMessage(order, items);

        await botClient.SendMessage(
            chatId: options.ChatId,
            text: message,
            parseMode: ParseMode.Html,
            cancellationToken: cancellationToken);

        logger.LogInformation(
            "Telegram notification sent for order {OrderId} to chat {ChatId}.",
            order.Id,
            options.ChatId);
    }

    private static string BuildMessage(Order order, List<OrderItem> items)
    {
        var productLines = items.Select(item =>
            $"• {item.Quantity} x {Encode(item.ProductName)} - {Encode(FormatMoney(item.UnitPrice * item.Quantity))} RUB");

        return string.Join('\n', [
            $"🛍 <b>НОВЫЙ ЗАКАЗ #{order.Id}</b>",
            string.Empty,
            $"👤 <b>Клиент:</b> {Encode(order.CustomerName)}",
            $"📞 <b>Телефон:</b> {Encode(order.CustomerPhone)}",
            $"📧 <b>Email:</b> {Encode(order.CustomerEmail)}",
            string.Empty,
            "🛒 <b>Товары:</b>",
            ..productLines,
            string.Empty,
            $"💰 <b>ИТОГО:</b> {Encode(FormatMoney(order.TotalAmount))} RUB",
            $"⏰ <b>Время:</b> {Encode(order.OrderDate.ToLocalTime().ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture))}"
        ]);
    }

    private static string FormatMoney(decimal amount)
    {
        return amount.ToString("N2", CultureInfo.InvariantCulture);
    }

    private static string Encode(string value)
    {
        return WebUtility.HtmlEncode(value);
    }
}
