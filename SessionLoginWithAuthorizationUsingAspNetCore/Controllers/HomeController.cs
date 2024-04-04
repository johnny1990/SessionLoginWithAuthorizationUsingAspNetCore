using Microsoft.AspNetCore.Mvc;
using SessionLoginWithAuthorizationUsingAspNetCore.Helpers;
using SessionLoginWithAuthorizationUsingAspNetCore.Models;
using System.Diagnostics;

namespace SessionLoginWithAuthorizationUsingAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        SessionLoginWithAuthorizationUsingAspNetCoreDbContext db = new SessionLoginWithAuthorizationUsingAspNetCoreDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authentication]
        [Authorization(Roles = "Admin")]
        public IActionResult Test()
        {
            //get all accounts from database
            ViewData["Accounts"] = db.Accounts.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
