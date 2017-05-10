using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface IPositionRepository
    {
        IEnumerable<Position> GetByTemplate(int templateId);
    }
}
