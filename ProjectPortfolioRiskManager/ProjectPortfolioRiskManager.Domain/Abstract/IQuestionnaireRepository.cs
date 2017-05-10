using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface IQuestionnaireRepository
    {
        IEnumerable<Questionnaire> GetByUser(string userName);
    }
}
