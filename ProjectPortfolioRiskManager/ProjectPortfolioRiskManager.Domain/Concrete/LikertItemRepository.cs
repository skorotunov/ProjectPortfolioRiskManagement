using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class LikertItemRepository : ILikertItemRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<LikertItem> GetByTemplate(int templateId)
        {
            return context.Templates.Single(x => x.Id == templateId).LikertItems.OrderBy(x => x.OrderNumber).Select(x => x.LikertItem);
        }
    }
}
