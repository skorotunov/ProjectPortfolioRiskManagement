using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class QuestionRepository : IQuestionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Question> GetByTemplate(int templateId)
        {
            var sectionIDs = context.Templates.Single(x => x.Id == templateId).Sections.Select(y => y.Id);
            return context.Questions.Where(x => sectionIDs.Contains(x.SectionId));
        }
    }
}
