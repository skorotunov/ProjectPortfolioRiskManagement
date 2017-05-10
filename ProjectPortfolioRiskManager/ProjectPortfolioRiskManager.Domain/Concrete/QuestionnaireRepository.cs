using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Questionnaire> GetByUser(string userName)
        {
            return context.Questionnaires.Where(x => x.UserId.Equals(userName));
        }
    }
}
