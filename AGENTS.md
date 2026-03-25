# Руководство для агента по разработке MVP интернет-магазина
## Стек: ASP.NET Core MVC, Feature Slice Architecture, PostgreSQL, Telegram Bot API

## 1. О документе
Этот файл предназначен для ИИ-агента (программиста), который будет генерировать код для минимально жизнеспособного продукта (MVP) интернет-магазина.

**Роль агента:** Ведущий .NET разработчик.
**Цель:** Создать работающий прототип на ASP.NET Core MVC с PostgreSQL и интеграцией Telegram-бота для уведомлений о заказах.
**Ключевые требования:**
- **Архитектура:** Feature Slice (организация по функциям/фичам, а не по техническим слоям)
- **База данных:** PostgreSQL (Entity Framework Core)
- **Шаблонизация:** Razor Views
- **Telegram:** Отправка сообщений в Telegram канал/чат при создании заказа
- **Работа с данными:** Прямые запросы через DbContext внутри фич

## 2. Общее описание проекта (Контекст)
Мы создаем простой интернет-магазин с минимальным набором функций, достаточным для запуска и тестирования гипотезы.

- **Название:** `MvpShop`
- **Основные сущности:** Товар (Product), Корзина (Cart), Заказ (Order)
- **Telegram:** Уведомления о новых заказах отправляются в указанный Telegram-чат
- **Дизайн:** Минималистичный на Bootstrap (встроен в ASP.NET Core шаблон)
- **База данных:** PostgreSQL. Все миграции через Entity Framework Core CLI.

## 3. Архитектура: Feature Slice

Проект НЕ должен быть разделен на стандартные папки (Controllers, Models, Views, Data). Вместо этого:


MvpShop/
├── Features/
│ ├── Products/
│ │ ├── List.cshtml
│ │ ├── List.cshtml.cs
│ │ ├── Details.cshtml
│ │ ├── Details.cshtml.cs
│ │ ├── Create.cshtml (админка)
│ │ └── Create.cshtml.cs
│ ├── Cart/
│ │ ├── Index.cshtml
│ │ ├── Index.cshtml.cs
│ │ ├── AddItem.cshtml.cs (API endpoint)
│ │ └── CartService.cs (логика корзины в cookies)
│ ├── Orders/
│ │ ├── Checkout.cshtml
│ │ ├── Checkout.cshtml.cs
│ │ ├── Confirmation.cshtml
│ │ └── OrderService.cs
├── Infrastructure/
│ ├── Telegram/
│ │ ├── ITelegramService.cs
│ │ ├── TelegramService.cs
│ │ └── TelegramSettings.cs
├── Data/
│ ├── AppDbContext.cs
│ ├── Entities/
│ │ ├── Product.cs
│ │ ├── Order.cs
│ │ └── OrderItem.cs
│ └── Migrations/
├── Shared/
│ ├── _Layout.cshtml
│ ├── _ViewStart.cshtml
│ └── TagHelpers/ (если нужны)
└── Program.cs




**Правило для агента:** 
- Каждая фича (Products, Cart, Orders) — это отдельная папка. Внутри фичи лежат все необходимые Razor Pages и сервисы.
- Инфраструктурный код (Telegram, email и т.д.) выносится в папку `Infrastructure`.

## 4. Список фич для разработки (Бэклог агента)

Агент должен реализовать следующие блоки строго по порядку.

### Этап 0: Настройка проекта
- **Фича 0.1: Инициализация решения.**
    - Создать новый проект ASP.NET Core MVC (без аутентификации).
    - Подключить NuGet пакеты: 
        - `Npgsql.EntityFrameworkCore.PostgreSQL`
        - `Microsoft.EntityFrameworkCore.Tools`
        - `Microsoft.EntityFrameworkCore.Design`
        - `Telegram.Bot` (для работы с Telegram API)
    - Настроить `appsettings.json` для строки подключения к PostgreSQL и Telegram настроек.
- **Фича 0.2: Структура папок.**
    - Создать папки: `Features`, `Infrastructure/Telegram`, `Data/Entities`, `Shared`.
    - Перенести стандартные `HomeController` и связанные Views в папку `Features/Home`.

### Этап 1: Товары (Products)
- **Фича 1.1: Модель товара.**
    - Создать класс `Product` в `Data/Entities` (Id, Name, Description, Price, ImageUrl, CreatedAt).
    - Добавить `DbSet<Product>` в `AppDbContext`.
    - Создать и применить миграцию.
- **Фича 1.2: Админка для товаров.**
    - В папке `Features/Products` создать Razor Pages для CRUD операций:
        - `Create.cshtml` — форма добавления товара.
        - `Edit.cshtml` — редактирование.
        - `Delete.cshtml` — удаление.
    - *Важно:* На данном этапе защиту не делаем, просто вешаем ссылку в футере.
- **Фича 1.3: Витрина товаров.**
    - `List.cshtml` — главная страница магазина (маршрут `/`). Выводит все товары из БД в виде карточек.
    - `Details.cshtml` — страница товара (маршрут `/products/{id}`). Детальное описание и кнопка "В корзину".

### Этап 2: Корзина (Cart)
- **Фича 2.1: Сервис корзины.**
    - Создать `CartService` в папке `Features/Cart`.
    - Корзина хранится в куках как зашифрованный JSON (список `CartItem` с ProductId, Name, Price, Quantity).
    - Методы: `AddToCart`, `RemoveFromCart`, `UpdateQuantity`, `GetCartItems`, `ClearCart`.
- **Фича 2.2: Добавление в корзину.**
    - Создать `AddItem.cshtml.cs`, который обрабатывает POST запрос с `productId`.
    - Использует `CartService` для добавления товара.
    - Возвращает JSON с новым количеством товаров в корзине.
- **Фича 2.3: Страница корзины.**
    - `Index.cshtml` — отображает список товаров в корзине, количество, цену и итог.
    - Кнопка "Очистить корзину" и кнопка "Оформить заказ".

### Этап 3: Оформление заказа (Orders)
- **Фича 3.1: Модели заказа.**
    - Создать классы `Order` и `OrderItem` в `Data/Entities`.
    - `Order`: Id, CustomerName, CustomerEmail, CustomerPhone, OrderDate, TotalAmount, Status (enum: Pending, Paid, Shipped).
    - `OrderItem`: Id, OrderId, ProductId, ProductName, UnitPrice, Quantity.
    - Добавить `DbSet<Order>` и `DbSet<OrderItem>` в контекст.
- **Фича 3.2: Страница оформления.**
    - `Checkout.cshtml` — форма для ввода имени, email, телефона.
    - При отправке:
        - Получить корзину из `CartService`.
        - Создать запись `Order` и связанные `OrderItem` в БД (в транзакции).
        - **Вызвать TelegramService для отправки уведомления.**
        - Очистить корзину.
        - Перенаправить на страницу подтверждения с `orderId`.
- **Фича 3.3: Подтверждение заказа.**
    - `Confirmation.cshtml` — страница "Спасибо за заказ!" с номером заказа.

### Этап 4: Telegram интеграция (Новый этап!)
- **Фича 4.1: Настройки Telegram.**
    - Создать класс `TelegramSettings` в `Infrastructure/Telegram` для привязки из конфигурации:
      ```csharp
      public class TelegramSettings
      {
          public string BotToken { get; set; } = string.Empty;
          public string ChatId { get; set; } = string.Empty; // Можно числом, но строкой удобнее
      }




**Фича 4.2: Сервис Telegram.**

Создать интерфейс ITelegramService с методом Task SendOrderNotificationAsync(Order order, List<OrderItem> items).
Реализовать TelegramService, используя библиотеку Telegram.Bot.
В методе формирования сообщения создать красивый текст с эмодзи:

🛍 *НОВЫЙ ЗАКАЗ #{orderId}*

👤 *Клиент:* {CustomerName}
📞 *Телефон:* {CustomerPhone}
📧 *Email:* {CustomerEmail}

🛒 *Товары:*
{foreach item in items}
• {item.Quantity} x {item.ProductName} — {item.UnitPrice * item.Quantity} ₽
{end}

💰 *ИТОГО:* {TotalAmount} ₽

⏰ *Время:* {OrderDate:dd.MM.yyyy HH:mm}