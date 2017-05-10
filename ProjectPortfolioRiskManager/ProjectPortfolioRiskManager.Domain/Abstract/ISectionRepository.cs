using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface ISectionRepository
    {
        IEnumerable<Section> GetByTemplate(int templateId);
    }
}
