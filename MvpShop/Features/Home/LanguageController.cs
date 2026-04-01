using Microsoft.AspNetCore.Mvc;
using MvpShop.Infrastructure.Localization;

namespace MvpShop.Features.Home;

public class LanguageController : Controller
{
    [HttpPost("language/set")]
    [ValidateAntiForgeryToken]
    public IActionResult Set(string language, string? returnUrl)
    {
        var normalized = language == AppLocalizer.Russian
            ? AppLocalizer.Russian
            : AppLocalizer.Mongolian;

        Response.Cookies.Append(
            AppLocalizer.CookieName,
            normalized,
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                SameSite = SameSiteMode.Lax
            });

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }

        return Redirect("/");
    }
}
