using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private EFUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<EFUserManager>();
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Name, model.Password);
                if (user == null)
                {
                    return View("Error", new string[] { "Invalid name or password" });
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);

                    if (User.IsInRole("Analytic") || User.IsInRole("Administrator"))
                    {
                        return RedirectToAction("Index", "Analitic");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Expert");
                    }
                }
            }
            return View(model);
        }
    }
}