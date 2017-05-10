using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetByTemplate(int templateId);
    }
}
