using Microsoft.AspNet.Identity.Owin;
using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize(Roles = "Expert, Administrator")]
    public class ExpertController : Controller
    {
        private readonly IQuestionnaireRepository questionnaireRepository;
        private readonly ITemplateRepository templateRepository;
        private readonly ICompanySizeRepository companySizeRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly ILikertItemRepository likertItemRepository;

        public ExpertController(IQuestionnaireRepository questionnaireRepository, ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository,
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
        public async Task<ActionResult> Index()
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            var model = new QuestionnaireViewModel(userId, questionnaireRepository, templateRepository, companySizeRepository, questionRepository);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(QuestionnaireViewModel model)
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            var userId = user.Id;
            model.Submit(model.TemplateId, model.CompanySizeId, model.PositionId, model.Industry, model.Answers, userId, model.Id, questionnaireRepository);
            return View(model);
        }

        [HttpGet]
        public ActionResult CompanySizePartial(int templateId, int companySizeId)
        {
            ViewBag.CompanySizeId = companySizeId;
            IEnumerable<CompanySize> model = companySizeRepository.GetByTemplate(templateId);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult PositionPartial(int templateId)
        {
            IEnumerable<Position> model = positionRepository.GetByTemplate(templateId);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult DynamicCapabilitiesPartial(int templateId, Dictionary<string, int> answers)
        {
            //get selected data here
            ViewBag.Answers = answers;
            var model = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository);
            return PartialView("SectionPartial", model);
        }

        [HttpGet]
        public ActionResult PortfolioRiskManagementPartial(int templateId)
        {
            var model = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository, true);
            return PartialView("SectionPartial", model);
        }

        private EFUserManager userManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<EFUserManager>(); }
        }
    }
}