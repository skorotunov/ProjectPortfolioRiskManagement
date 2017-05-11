using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface IQuestionnaireRepository
    {
        IEnumerable<Questionnaire> GetByUser(string userId);
        string Save(int templateId, int companySizeId, int positionId, string industry, Dictionary<string, int?> answers, string userId, int? id);
    }
}
