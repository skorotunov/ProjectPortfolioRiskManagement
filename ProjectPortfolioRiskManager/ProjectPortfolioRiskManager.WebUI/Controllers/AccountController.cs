using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<ViewResult> Index()
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            var model = new EditProfileViewModel(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(User.Identity.Name);
                user.UserName = model.Name;
                user.Email = model.Email;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager authManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private EFUserManager userManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<EFUserManager>(); }
        }
    }
}