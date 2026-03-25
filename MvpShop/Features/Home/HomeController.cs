using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MvpShop.Features.Home;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("List", "Products");
    }

    [HttpGet("privacy")]
    public IActionResult Privacy()
    {
        return View("~/Features/Home/Privacy.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet("error")]
    public IActionResult Error()
    {
        return View("~/Shared/Error.cshtml", new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
