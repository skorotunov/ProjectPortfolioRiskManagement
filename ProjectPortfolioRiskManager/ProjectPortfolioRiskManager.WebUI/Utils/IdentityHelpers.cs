using Microsoft.AspNet.Identity.Owin;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Utils
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, string id)
        {
            var mgr = HttpContext.Current.GetOwinContext().GetUserManager<EFUserManager>();
            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}