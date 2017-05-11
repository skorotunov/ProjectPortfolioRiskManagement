using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface IAnswerRepository
    {
        IEnumerable<Answer> GetByQuestionnaire(int questionnaireId);
    }
}
