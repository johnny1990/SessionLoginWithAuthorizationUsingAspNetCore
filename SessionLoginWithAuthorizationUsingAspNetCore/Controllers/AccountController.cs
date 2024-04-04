using Microsoft.AspNetCore.Mvc;
using SessionLoginWithAuthorizationUsingAspNetCore.Helpers;
using SessionLoginWithAuthorizationUsingAspNetCore.Models;

namespace SessionLoginWithAuthorizationUsingAspNetCore.Controllers
{
    public class AccountController : Controller
    {
        SessionLoginWithAuthorizationUsingAspNetCoreDbContext db = new SessionLoginWithAuthorizationUsingAspNetCoreDbContext();
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var account = CheckAccount(username, password);
            if (account == null)
            {
                ViewBag.Error = "Invalid account";
                return View("Login");
            }
            else
            {
                HttpContext.Session.SetString("username", username);
                return View("Welcome");
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Account account)
        {
            string userCheck = (from a in db.Accounts
                                where a.UserName == account.UserName
                                select new
                                {
                                    a.UserName
                                }).ToString();
            {
               
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Authentication]
        public IActionResult ChangePassword()
        {//luam valorile din sesiune
            var username = HttpContext.Session.GetString("username");
            ViewBag.Username = username;
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(InputModel input)
        {   
            var username = HttpContext.Session.GetString("username");
            var accountInDb = db.Accounts.SingleOrDefault(a => a.UserName.Equals(username));
            accountInDb.Password = BCrypt.Net.BCrypt.HashPassword(input.NewPassword);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        private Account CheckAccount(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.UserName.Equals(username));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }
    }
}
