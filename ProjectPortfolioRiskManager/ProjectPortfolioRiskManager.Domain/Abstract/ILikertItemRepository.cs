using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface ILikertItemRepository
    {
        IEnumerable<LikertItem> GetByTemplate(int templateId);
    }
}
