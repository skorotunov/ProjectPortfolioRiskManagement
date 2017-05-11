using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class PositionRepository : IPositionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Position> GetByTemplate(int templateId)
        {
            return context.Templates.Single(x => x.Id == templateId).Positions.OrderBy(x => x.Id);
        }
    }
}
