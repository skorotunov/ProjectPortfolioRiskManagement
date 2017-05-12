using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.BLL;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ProjectPortfolioRiskManager.WebUI.Utils.Enums;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize(Roles = "Analytic, Administrator")]
    public class AnalyticController : Controller
    {
        private readonly IQuestionnaireRepository questionnaireRepository;
        private readonly ITemplateRepository templateRepository;
        private readonly ICompanySizeRepository companySizeRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly ILikertItemRepository likertItemRepository;

        public AnalyticController(IQuestionnaireRepository questionnaireRepository, ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository,
            IPositionRepository positionRepository, ISectionRepository sectionRepository, IQuestionRepository questionRepository, ILikertItemRepository likertItemRepository)
        {
            this.questionnaireRepository = questionnaireRepository;
            this.templateRepository = templateRepository;
            this.companySizeRepository = companySizeRepository;
            this.positionRepository = positionRepository;
            this.sectionRepository = sectionRepository;
            this.questionRepository = questionRepository;
            this.likertItemRepository = likertItemRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new EditTemplateViewModel(RenderRazorViewToString, templateRepository, companySizeRepository, positionRepository, sectionRepository, questionRepository, likertItemRepository);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(EditTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(User.Identity.Name);
                string userId = user.Id;
                model.Submit(userId, templateRepository, companySizeRepository, positionRepository, sectionRepository, questionRepository, likertItemRepository);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Users()
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
                    return RedirectToAction("Users");
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
                    return RedirectToAction("Users");
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
                return RedirectToAction("Users");
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
                            return RedirectToAction("Users");
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
            var model = new List<RoleViewModel>();
            foreach (Role role in roleManager.Roles)
            {
                model.Add(new RoleViewModel(role, userManager));
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
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

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EditRole(string id)
        {
            Role role = await roleManager.FindByIdAsync(id);
            var model = new EditRoleViewModel(role, userManager, roleManager);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EditRole(RoleModificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    result = await userManager.AddToRoleAsync(userId, model.Name);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    result = await userManager.RemoveFromRoleAsync(userId, model.Name);
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

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}