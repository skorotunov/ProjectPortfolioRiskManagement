using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetByTemplate(int templateId);
        void UpdateValues(int templateId, List<string> list);
        void InsertValues(int templateId, List<string> list, List<int> sectionsMapping);
    }
}
