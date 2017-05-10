using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.WebUI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize(Roles = "Expert, Administrator")]
    public class ExpertController : Controller
    {
        private readonly ITemplateRepository templateRepository;
        private readonly ICompanySizeRepository companySizeRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ISectionRepository sectionRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly ILikertItemRepository likertItemRepository;

        public ExpertController(ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository, IPositionRepository positionRepository,
            ISectionRepository sectionRepository, IQuestionRepository questionRepository, ILikertItemRepository likertItemRepository)
        {
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
            var template = templateRepository.Get();
            var model = new QuestionnaireViewModel(template);
            return View(model);
        }

        [HttpGet]
        public ActionResult CompanySizePartial(int templateId)
        {
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
        public ActionResult DynamicCapabilitiesPartial(int templateId)
        {
            var model = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository);
            return PartialView("SectionPartial", model);
        }

        [HttpGet]
        public ActionResult PortfolioRiskManagementPartial(int templateId)
        {
            var model = new SectionViewModel(templateId, sectionRepository, questionRepository, likertItemRepository, true);
            return PartialView("SectionPartial", model);
        }
    }
}