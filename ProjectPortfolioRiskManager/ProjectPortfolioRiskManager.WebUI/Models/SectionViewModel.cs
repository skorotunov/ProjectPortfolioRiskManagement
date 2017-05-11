using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class SectionViewModel
    {
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<LikertItem> LikertItems { get; set; }

        public SectionViewModel()
        { }

        public SectionViewModel(int templateId, ISectionRepository sectionRepository, IQuestionRepository questionRepository,
            ILikertItemRepository likertItemRepository, bool initPRMSection = false)
        {
            if (initPRMSection)
            {
                Sections = sectionRepository.GetByTemplate(templateId).Where(x => x.Name.Equals("Risk identification and classification"));
            }
            else
            {
                Sections = sectionRepository.GetByTemplate(templateId).Where(x => !x.Name.Equals("Risk identification and classification"));
            }
            Questions = questionRepository.GetByTemplate(templateId);
            LikertItems = likertItemRepository.GetByTemplate(templateId);
        }
    }
}