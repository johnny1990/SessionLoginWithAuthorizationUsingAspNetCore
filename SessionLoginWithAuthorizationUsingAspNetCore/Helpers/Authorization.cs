using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SessionLoginWithAuthorizationUsingAspNetCore.Models;

namespace SessionLoginWithAuthorizationUsingAspNetCore.Helpers
{
    public class Authorization : ActionFilterAttribute
    {
        SessionLoginWithAuthorizationUsingAspNetCoreDbContext db = new SessionLoginWithAuthorizationUsingAspNetCoreDbContext();
        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("username") != null)
            {
                var user = filterContext.HttpContext.Session.GetString("username");

                var role = (from a in db.Accounts
                            join ur in db.UserRoles on a.Id equals ur.UserId
                            join r in db.Roles on ur.RoleId equals r.Id
                            where a.UserName == user
                            select r.Name).First().ToString();

                if (!Roles.Contains(role))
                {
                    filterContext.Result = new RedirectToRouteResult(
                                           new RouteValueDictionary
                                           {
                                { "Controller", "Account" },
                                { "Action", "AccessDenied" }
                            });
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
        }
    }
}
