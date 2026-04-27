using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Filters
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext != null && httpContext.Session != null && httpContext.Session["adminid"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Admin/Login");
        }
    }
}
