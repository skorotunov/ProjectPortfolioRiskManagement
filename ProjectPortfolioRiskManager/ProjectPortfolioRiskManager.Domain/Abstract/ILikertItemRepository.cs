using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface ILikertItemRepository
    {
        IEnumerable<LikertItem> GetByTemplate(int templateId);
        void UpdateValues(int templateId, List<string> list);
        void InsertValues(int templateId, List<string> list);
    }
}
