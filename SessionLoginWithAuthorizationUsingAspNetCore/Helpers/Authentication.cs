using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SessionLoginWithAuthorizationUsingAspNetCore.Helpers
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("username") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                                { "Controller", "Account" },
                                { "Action", "Login" }
                            });
            }

        }
    }
}
