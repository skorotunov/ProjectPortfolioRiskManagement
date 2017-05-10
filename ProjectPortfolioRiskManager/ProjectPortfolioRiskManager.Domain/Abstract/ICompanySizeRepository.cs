using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface ICompanySizeRepository
    {
        IEnumerable<CompanySize> GetByTemplate(int templateId);
    }
}
