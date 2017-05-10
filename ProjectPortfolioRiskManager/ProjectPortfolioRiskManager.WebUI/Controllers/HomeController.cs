using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ProjectPortfolioRiskManager.WebUI.Utils.Enums;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindAsync(model.Name, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password");
                }
                else
                {
                    ClaimsIdentity ident = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignOut();
                    authManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);

                    if (User.IsInRole(eRoles.Expert.ToString()))
                    {
                        return RedirectToAction("Index", "Expert");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Analytic");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Name, Email = model.Email };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    IdentityResult roleSettingResult = await userManager.AddToRoleAsync(user.Id, eRoles.Expert.ToString());
                    if (!roleSettingResult.Succeeded)
                    {
                        addErrorsFromResult(roleSettingResult);
                    }
                    return RedirectToAction("Index", "Expert");
                }
                else
                {
                    addErrorsFromResult(result);
                }
            }
            return View(model);
        }

        private IAuthenticationManager authManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private EFUserManager userManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<EFUserManager>(); }
        }

        private void addErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}