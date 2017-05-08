using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.BLL;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ProjectPortfolioRiskManager.WebUI.Utils.Enums;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize(Roles = "Analytic, Administrator")]
    public class AnalyticController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new List<UserViewModel>();
            IEnumerable<User> users = RoleLogic.GetNonRoleMembers(userManager, roleManager, eRoles.Administrator.ToString());
            foreach (var user in users)
            {
                model.Add(new UserViewModel(user));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserViewModel model)
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
                    return RedirectToAction("Index");
                }
                else
                {
                    addErrorsFromResult(result);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var model = new EditUserViewModel(user);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.UserName = model.Name;
                    user.Email = model.Email;
                    IdentityResult validEmail = await userManager.UserValidator.ValidateAsync(user);
                    if (!validEmail.Succeeded)
                    {
                        addErrorsFromResult(validEmail);
                    }
                    IdentityResult validPass = null;
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        validPass = await userManager.PasswordValidator.ValidateAsync(model.Password);
                        if (validPass.Succeeded)
                        {
                            user.PasswordHash = userManager.PasswordHasher.HashPassword(model.Password);
                        }
                        else
                        {
                            addErrorsFromResult(validPass);
                        }
                    }
                    if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && !string.IsNullOrEmpty(model.Password) && validPass.Succeeded))
                    {
                        IdentityResult result = await userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            addErrorsFromResult(result);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Roles()
        {
            return View(roleManager.Roles);
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new Role(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    addErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(string id)
        {
            Role role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Role Not Found" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditRole(string id)
        {
            Role role = await roleManager.FindByIdAsync(id);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<User> members = userManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            IEnumerable<User> nonMembers = userManager.Users.Except(members);
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await userManager.AddToRoleAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await userManager.RemoveFromRoleAsync(userId, model.RoleName);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Roles");
            }
            return View("Error", new string[] { "Role Not Found" });
        }

        private EFUserManager userManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<EFUserManager>(); }
        }

        private EFRoleManager roleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<EFRoleManager>(); }
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