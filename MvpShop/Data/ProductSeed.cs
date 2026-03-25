using MvpShop.Data.Entities;

namespace MvpShop.Data;

public static class ProductSeed
{
    public static IReadOnlyList<Product> MongolianProducts => [
        new Product
        {
            Name = "Сүү",
            Description = "Ангилал: Сүүн бүтээгдэхүүн. Топ бүтээгдэхүүн: ЭкоНива 3.2% тослогтой пастержуулсан ундны сүү. Савлагаа: 1 л. Худалдан авалтын эрэмбэ: 1.",
            Price = 100m,
            ImageUrl = "https://placehold.co/900x600/e9f3ff/1d3557?text=%D0%A1%D2%AF%D2%AF"
        },
        new Product
        {
            Name = "Хэрчсэн талх",
            Description = "Ангилал: Талх, нарийн боов. Топ бүтээгдэхүүн: ЗАО Хлеб дээд зэрэглэлийн хэрчсэн батон, 330 г. Савлагаа: 330 г. Худалдан авалтын эрэмбэ: 2.",
            Price = 54m,
            ImageUrl = "https://placehold.co/900x600/f6ead1/6b4f2a?text=%D0%A2%D0%B0%D0%BB%D1%85"
        },
        new Product
        {
            Name = "Тахианы цээжний цул мах",
            Description = "Ангилал: Мах. Топ бүтээгдэхүүн: Петелинка хөргөсөн, арьсгүй тахианы цээжний филе. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 3.",
            Price = 600m,
            ImageUrl = "https://placehold.co/900x600/f7d9d9/7f1d1d?text=%D0%A2%D0%B0%D1%85%D0%B8%D0%B0"
        },
        new Product
        {
            Name = "Төмс",
            Description = "Ангилал: Хүнсний ногоо. Топ бүтээгдэхүүн: Улаан төмс. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 4.",
            Price = 80m,
            ImageUrl = "https://placehold.co/900x600/e3d2a1/5a4a1b?text=%D0%A2%D3%A9%D0%BC%D1%81"
        },
        new Product
        {
            Name = "Сонгино",
            Description = "Ангилал: Хүнсний ногоо. Топ бүтээгдэхүүн: Бөөрөнхий сонгино. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 5.",
            Price = 50m,
            ImageUrl = "https://placehold.co/900x600/f3e3a1/6b5b10?text=%D0%A1%D0%BE%D0%BD%D0%B3%D0%B8%D0%BD%D0%BE"
        },
        new Product
        {
            Name = "Лууван",
            Description = "Ангилал: Хүнсний ногоо. Топ бүтээгдэхүүн: Шороотой лууван. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 6.",
            Price = 40m,
            ImageUrl = "https://placehold.co/900x600/f8d2a2/8a3b12?text=%D0%9B%D1%83%D1%83%D0%B2%D0%B0%D0%BD"
        },
        new Product
        {
            Name = "Алим",
            Description = "Ангилал: Жимс. Топ бүтээгдэхүүн: Молдов улсад үйлдвэрлэсэн Айдаред алим. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 7.",
            Price = 140m,
            ImageUrl = "https://placehold.co/900x600/dbeec7/355e3b?text=%D0%90%D0%BB%D0%B8%D0%BC"
        },
        new Product
        {
            Name = "Банана",
            Description = "Ангилал: Жимс. Топ бүтээгдэхүүн: Эквадор банана. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 8.",
            Price = 160m,
            ImageUrl = "https://placehold.co/900x600/fff0a6/6b5f00?text=%D0%91%D0%B0%D0%BD%D0%B0%D0%BD%D0%B0"
        },
        new Product
        {
            Name = "Жүрж",
            Description = "Ангилал: Жимс. Топ бүтээгдэхүүн: Энэтхэг жүрж. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 9.",
            Price = 180m,
            ImageUrl = "https://placehold.co/900x600/ffd6a5/9a3412?text=%D0%96%D2%AF%D1%80%D0%B6"
        },
        new Product
        {
            Name = "Жигнэмэг",
            Description = "Ангилал: Кондитер. Топ бүтээгдэхүүн: Яшкино тослог овъёосон жигнэмэг. Савлагаа: 400 г. Худалдан авалтын эрэмбэ: 10.",
            Price = 99m,
            ImageUrl = "https://placehold.co/900x600/efe1c6/6b4f2a?text=%D0%96%D0%B8%D0%B3%D0%BD%D1%8D%D0%BC%D1%8D%D0%B3"
        },
        new Product
        {
            Name = "Чипс",
            Description = "Ангилал: Хөнгөн зууш. Топ бүтээгдэхүүн: Lay's беконтой төмсний чипс. Савлагаа: 81 г. Худалдан авалтын эрэмбэ: 11.",
            Price = 120m,
            ImageUrl = "https://placehold.co/900x600/fde68a/854d0e?text=%D0%A7%D0%B8%D0%BF%D1%81"
        },
        new Product
        {
            Name = "Хийжүүлээгүй ус",
            Description = "Ангилал: Архигүй ундаа. Топ бүтээгдэхүүн: Святой Источник хийжүүлээгүй ус. Савлагаа: 1.5 л. Худалдан авалтын эрэмбэ: 12.",
            Price = 69m,
            ImageUrl = "https://placehold.co/900x600/dbeafe/1d4ed8?text=%D0%A3%D1%81"
        },
        new Product
        {
            Name = "Хөлдөөсөн бууз",
            Description = "Ангилал: Хөлдөөсөн бүтээгдэхүүн. Топ бүтээгдэхүүн: Сибирская коллекция Сибирские банш. Савлагаа: 700 г. Худалдан авалтын эрэмбэ: 13.",
            Price = 290m,
            ImageUrl = "https://placehold.co/900x600/e5e7eb/374151?text=%D0%A5%D3%A9%D0%BB%D0%B4%D3%A9%D3%A9%D1%81%D3%A9%D0%BD"
        },
        new Product
        {
            Name = "Цай",
            Description = "Ангилал: Ундаа. Топ бүтээгдэхүүн: Принцесса Нури Высокогорный хар цай, 25 уут. Савлагаа: 50 г. Худалдан авалтын эрэмбэ: 14.",
            Price = 56m,
            ImageUrl = "https://placehold.co/900x600/d6c7a1/5b4636?text=%D0%A6%D0%B0%D0%B9"
        },
        new Product
        {
            Name = "Чихэр",
            Description = "Ангилал: Кондитер. Топ бүтээгдэхүүн: Красный Октябрь Маска чихэр. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 15.",
            Price = 374m,
            ImageUrl = "https://placehold.co/900x600/fbcfe8/9d174d?text=%D0%A7%D0%B8%D1%85%D1%8D%D1%80"
        },
        new Product
        {
            Name = "Наранцэцгийн тос",
            Description = "Ангилал: Хуурай хүнс. Топ бүтээгдэхүүн: Слобода дээд сортын цэвэршүүлсэн, үнэргүйжүүлсэн наранцэцгийн тос. Савлагаа: 1 л. Худалдан авалтын эрэмбэ: 16.",
            Price = 149m,
            ImageUrl = "https://placehold.co/900x600/fef3c7/92400e?text=%D0%A2%D0%BE%D1%81"
        },
        new Product
        {
            Name = "Йогурт",
            Description = "Ангилал: Сүүн бүтээгдэхүүн. Топ бүтээгдэхүүн: Актибио 3.5% тослогтой, B.Lactis-тэй био-йогурт. Савлагаа: 130 г. Худалдан авалтын эрэмбэ: 17.",
            Price = 56m,
            ImageUrl = "https://placehold.co/900x600/e0f2fe/0f766e?text=%D0%99%D0%BE%D0%B3%D1%83%D1%80%D1%82"
        },
        new Product
        {
            Name = "Зайдас",
            Description = "Ангилал: Хиам, зайдас. Топ бүтээгдэхүүн: Дымов Молочные чанасан зайдас. Савлагаа: 464 г. Худалдан авалтын эрэмбэ: 18.",
            Price = 240m,
            ImageUrl = "https://placehold.co/900x600/fbc4ab/7c2d12?text=%D0%97%D0%B0%D0%B9%D0%B4%D0%B0%D1%81"
        },
        new Product
        {
            Name = "Докторская хиам",
            Description = "Ангилал: Хиам, зайдас. Топ бүтээгдэхүүн: Окраина ангилал А чанасан Докторская хиам. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 19.",
            Price = 999m,
            ImageUrl = "https://placehold.co/900x600/f5c2c7/881337?text=%D0%A5%D0%B8%D0%B0%D0%BC"
        },
        new Product
        {
            Name = "Шар айраг",
            Description = "Ангилал: Согтууруулах ундаа. Топ бүтээгдэхүүн: Жигулевское цайвар, шүүсэн, пастержуулсан шар айраг. Савлагаа: 450 мл. Худалдан авалтын эрэмбэ: 20.",
            Price = 59m,
            ImageUrl = "https://placehold.co/900x600/fde68a/7c2d12?text=%D0%A8%D0%B0%D1%80+%D0%B0%D0%B9%D1%80%D0%B0%D0%B3"
        },
        new Product
        {
            Name = "Цөцгийн тос",
            Description = "Ангилал: Сүүн бүтээгдэхүүн. Топ бүтээгдэхүүн: Рузское молоко 82.5% тослогтой уламжлалт цөцгийн тос. Савлагаа: 175 г. Худалдан авалтын эрэмбэ: 21.",
            Price = 499m,
            ImageUrl = "https://placehold.co/900x600/fff7cc/8b6f00?text=%D0%A6%D3%A9%D1%86%D0%B3%D0%B8%D0%B9%D0%BD+%D1%82%D0%BE%D1%81"
        },
        new Product
        {
            Name = "Яргай загас",
            Description = "Ангилал: Загас. Топ бүтээгдэхүүн: Русское море бага давстай яргай загасны филе-зүсмэл. Савлагаа: 120 г. Худалдан авалтын эрэмбэ: 22.",
            Price = 499m,
            ImageUrl = "https://placehold.co/900x600/fbcfe8/9d174d?text=%D0%AF%D1%80%D0%B3%D0%B0%D0%B9"
        },
        new Product
        {
            Name = "Энергийн ундаа",
            Description = "Ангилал: Согтууруулах ундаа. Топ бүтээгдэхүүн: Red Bull Energy Drink. Савлагаа: 250 мл. Худалдан авалтын эрэмбэ: 23.",
            Price = 139m,
            ImageUrl = "https://placehold.co/900x600/dbeafe/1e3a8a?text=%D0%AD%D0%BD%D0%B5%D1%80%D0%B3%D0%B8"
        },
        new Product
        {
            Name = "Протейн батончик",
            Description = "Ангилал: Эрүүл хооллолт. Топ бүтээгдэхүүн: SNAQ FABRIQ Qwikler зөөлөн грильяж, газрын самартай, бүрмэл. Савлагаа: 40 г. Худалдан авалтын эрэмбэ: 24.",
            Price = 99m,
            ImageUrl = "https://placehold.co/900x600/e9d5ff/6b21a8?text=%D0%9F%D1%80%D0%BE%D1%82%D0%B5%D0%B9%D0%BD"
        },
        new Product
        {
            Name = "Хар перец",
            Description = "Ангилал: Амтлагч. Топ бүтээгдэхүүн: Индана хар перец. Савлагаа: 15 г. Худалдан авалтын эрэмбэ: 25.",
            Price = 69m,
            ImageUrl = "https://placehold.co/900x600/e5e7eb/111827?text=%D0%9F%D0%B5%D1%80%D0%B5%D1%86"
        },
        new Product
        {
            Name = "Шанцай",
            Description = "Ангилал: Амтлагч. Топ бүтээгдэхүүн: KOTANYI нунтаг шанцай. Савлагаа: 10 г. Худалдан авалтын эрэмбэ: 26.",
            Price = 65m,
            ImageUrl = "https://placehold.co/900x600/f5e1c8/92400e?text=%D0%A8%D0%B0%D0%BD%D1%86%D0%B0%D0%B9"
        },
        new Product
        {
            Name = "Давсалсан сельдь",
            Description = "Ангилал: Загасны бүтээгдэхүүн. Топ бүтээгдэхүүн: Балтийский берег По-царски тосон дахь бага давстай сельдийн филе. Савлагаа: 250 г. Худалдан авалтын эрэмбэ: 27.",
            Price = 176m,
            ImageUrl = "https://placehold.co/900x600/dbeafe/1d4ed8?text=%D0%A1%D0%B5%D0%BB%D1%8C%D0%B4%D1%8C"
        },
        new Product
        {
            Name = "Элсэн чихэр",
            Description = "Ангилал: Хуурай хүнс. Топ бүтээгдэхүүн: Продимекс цагаан элсэн чихэр. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 28.",
            Price = 60m,
            ImageUrl = "https://placehold.co/900x600/f8fafc/64748b?text=%D0%A1%D0%B0%D1%85%D0%B0%D1%80"
        },
        new Product
        {
            Name = "Гурил",
            Description = "Ангилал: Хуурай хүнс. Топ бүтээгдэхүүн: Makfa дээд сортын улаан буудайн гурил. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 29.",
            Price = 73m,
            ImageUrl = "https://placehold.co/900x600/faf5e6/78716c?text=%D0%93%D1%83%D1%80%D0%B8%D0%BB"
        },
        new Product
        {
            Name = "Спагетти",
            Description = "Ангилал: Гоймон. Топ бүтээгдэхүүн: Barilla Capellini No.1 хатуу улаан буудайн спагетти. Савлагаа: 450 г. Худалдан авалтын эрэмбэ: 30.",
            Price = 99m,
            ImageUrl = "https://placehold.co/900x600/fef3c7/92400e?text=%D0%A1%D0%BF%D0%B0%D0%B3%D0%B5%D1%82%D1%82%D0%B8"
        },
        new Product
        {
            Name = "Лаазалсан эрдэнэ шиш",
            Description = "Ангилал: Лаазалсан бүтээгдэхүүн. Топ бүтээгдэхүүн: Bonduelle амтат эрдэнэ шиш. Савлагаа: 340 г. Худалдан авалтын эрэмбэ: 31.",
            Price = 156m,
            ImageUrl = "https://placehold.co/900x600/fef08a/854d0e?text=%D0%AD%D1%80%D0%B4%D1%8D%D0%BD%D1%8D+%D1%88%D0%B8%D1%88"
        },
        new Product
        {
            Name = "Лаазалсан туна",
            Description = "Ангилал: Лаазалсан бүтээгдэхүүн. Топ бүтээгдэхүүн: Вкусные консервы байгалийн жижиглэсэн туна. Савлагаа: 185 г. Худалдан авалтын эрэмбэ: 32.",
            Price = 139m,
            ImageUrl = "https://placehold.co/900x600/e0f2fe/155e75?text=%D0%A2%D1%83%D0%BD%D0%B0"
        },
        new Product
        {
            Name = "Гречка",
            Description = "Ангилал: Үр тариа. Топ бүтээгдэхүүн: Увелка Экстра бүхэл гречка. Савлагаа: 500 г. Худалдан авалтын эрэмбэ: 33.",
            Price = 64m,
            ImageUrl = "https://placehold.co/900x600/ede9d5/57534e?text=%D0%93%D1%80%D0%B5%D1%87%D0%BA%D0%B0"
        },
        new Product
        {
            Name = "Будаа",
            Description = "Ангилал: Үр тариа. Топ бүтээгдэхүүн: Агро-Альянс Экстра пловны будаа. Савлагаа: 900 г. Худалдан авалтын эрэмбэ: 34.",
            Price = 129m,
            ImageUrl = "https://placehold.co/900x600/f8fafc/475569?text=%D0%91%D1%83%D0%B4%D0%B0%D0%B0"
        },
        new Product
        {
            Name = "Улаан чечевица",
            Description = "Ангилал: Буурцагт ургамал. Топ бүтээгдэхүүн: Мистраль Персидская улаан хуваасан чечевица. Савлагаа: 450 г. Худалдан авалтын эрэмбэ: 35.",
            Price = 154m,
            ImageUrl = "https://placehold.co/900x600/fecaca/991b1b?text=%D0%A7%D0%B5%D1%87%D0%B5%D0%B2%D0%B8%D1%86%D0%B0"
        },
        new Product
        {
            Name = "Овъёос",
            Description = "Ангилал: Үр тариа. Топ бүтээгдэхүүн: Русский Продукт Геркулес уламжлалт овъёосны х flakes. Савлагаа: 500 г. Худалдан авалтын эрэмбэ: 36.",
            Price = 85m,
            ImageUrl = "https://placehold.co/900x600/f5f5dc/6b7280?text=%D0%9E%D0%B2%D1%8A%D1%91%D0%BE%D1%81"
        },
        new Product
        {
            Name = "Бяслаг",
            Description = "Ангилал: Сүүн бүтээгдэхүүн. Топ бүтээгдэхүүн: Брест-Литовск Российский 50% хагас хатуу бяслаг. Савлагаа: 200 г. Худалдан авалтын эрэмбэ: 37.",
            Price = 189m,
            ImageUrl = "https://placehold.co/900x600/fef9c3/854d0e?text=%D0%91%D1%8F%D1%81%D0%BB%D0%B0%D0%B3"
        },
        new Product
        {
            Name = "Газрын самар",
            Description = "Ангилал: Хөнгөн зууш. Топ бүтээгдэхүүн: Джаз давсалсан шарсан газрын самар. Савлагаа: 150 г. Худалдан авалтын эрэмбэ: 38.",
            Price = 143m,
            ImageUrl = "https://placehold.co/900x600/e7d3b1/78350f?text=%D0%A1%D0%B0%D0%BC%D0%B0%D1%80"
        },
        new Product
        {
            Name = "Мюсли батончик",
            Description = "Ангилал: Снек. Топ бүтээгдэхүүн: Здоровый Перекус цангистай мюсли батончик. Савлагаа: 55 г. Худалдан авалтын эрэмбэ: 39.",
            Price = 39m,
            ImageUrl = "https://placehold.co/900x600/f5e6cc/9a3412?text=%D0%9C%D1%8E%D1%81%D0%BB%D0%B8"
        },
        new Product
        {
            Name = "Брокколи",
            Description = "Ангилал: Хөлдөөсөн бүтээгдэхүүн. Топ бүтээгдэхүүн: Морозко Green хөлдөөсөн брокколи. Савлагаа: 400 г. Худалдан авалтын эрэмбэ: 40.",
            Price = 198m,
            ImageUrl = "https://placehold.co/900x600/d9f99d/166534?text=%D0%91%D1%80%D0%BE%D0%BA%D0%BA%D0%BE%D0%BB%D0%B8"
        },
        new Product
        {
            Name = "Хумус",
            Description = "Ангилал: Ногооны зууш. Топ бүтээгдэхүүн: Хушны самартай хумус. Савлагаа: 200 г. Худалдан авалтын эрэмбэ: 41.",
            Price = 124m,
            ImageUrl = "https://placehold.co/900x600/efe1c6/92400e?text=%D0%A5%D1%83%D0%BC%D1%83%D1%81"
        },
        new Product
        {
            Name = "Гахайн хүзүү мах",
            Description = "Ангилал: Мах. Топ бүтээгдэхүүн: ЧЕРКИЗОВО По-баварски сүмстэй гахайн хүзүү мах. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 42.",
            Price = 799m,
            ImageUrl = "https://placehold.co/900x600/fbc4ab/7f1d1d?text=%D0%93%D0%B0%D1%85%D0%B0%D0%B9"
        },
        new Product
        {
            Name = "Шинэ ногооны багц",
            Description = "Ангилал: Ногоон навчит ургамал. Топ бүтээгдэхүүн: Яншуй, укроп, ногоон сонгины багц. Савлагаа: 100 г. Худалдан авалтын эрэмбэ: 43.",
            Price = 89m,
            ImageUrl = "https://placehold.co/900x600/d9f99d/166534?text=%D0%9D%D0%BE%D0%B3%D0%BE%D0%BE%D0%BD"
        },
        new Product
        {
            Name = "Лимон",
            Description = "Ангилал: Жимс. Топ бүтээгдэхүүн: Лимон. Савлагаа: 1 кг. Худалдан авалтын эрэмбэ: 44.",
            Price = 259m,
            ImageUrl = "https://placehold.co/900x600/fef08a/854d0e?text=%D0%9B%D0%B8%D0%BC%D0%BE%D0%BD"
        },
        new Product
        {
            Name = "Кофе",
            Description = "Ангилал: Ундаа. Топ бүтээгдэхүүн: Jardin Gold уусдаг хөлдөөж хатаасан кофе. Савлагаа: 95 г. Худалдан авалтын эрэмбэ: 45.",
            Price = 399m,
            ImageUrl = "https://placehold.co/900x600/e7d3b1/4b2e16?text=%D0%9A%D0%BE%D1%84%D0%B5"
        },
        new Product
        {
            Name = "Дарс",
            Description = "Ангилал: Согтууруулах ундаа. Топ бүтээгдэхүүн: Фанагория Резерв Шардоне цагаан хагас чихэрлэг дарс. Савлагаа: 750 мл. Худалдан авалтын эрэмбэ: 46.",
            Price = 369m,
            ImageUrl = "https://placehold.co/900x600/f3d1d8/7f1d1d?text=%D0%94%D0%B0%D1%80%D1%81"
        }
    ];
}
