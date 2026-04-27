using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace KurumsalWeb.Filters
{
    public class SafeValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            try
            {
                AntiForgery.Validate();
            }
            catch (HttpAntiForgeryException)
            {
                // Clear stale anti-forgery cookie and redirect back to login.
                var expiredCookie = new HttpCookie(AntiForgeryConfig.CookieName)
                {
                    Expires = DateTime.UtcNow.AddDays(-1),
                    Value = string.Empty,
                    Path = "/"
                };

                filterContext.HttpContext.Response.Cookies.Add(expiredCookie);
                filterContext.Controller.TempData["Alert"] = "Session token was refreshed. Please try login again.";
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Admin", action = "Login" }));
            }
        }
    }
}
