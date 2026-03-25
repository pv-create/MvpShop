using MvpShop.Data.Entities;

namespace MvpShop.Infrastructure.Telegram;

public interface ITelegramService
{
    Task SendOrderNotificationAsync(Order order, List<OrderItem> items, CancellationToken cancellationToken = default);
}
