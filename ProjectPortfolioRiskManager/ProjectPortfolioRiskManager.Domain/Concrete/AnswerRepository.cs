using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class AnswerRepository : IAnswerRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Answer> GetByQuestionnaire(int questionnaireId)
        {
            return context.Answers.Where(x => x.QuestionnaireId == questionnaireId).OrderBy(x => x.Question.SectionId).ThenBy(x => x.QuestionId);
        }
    }
}
