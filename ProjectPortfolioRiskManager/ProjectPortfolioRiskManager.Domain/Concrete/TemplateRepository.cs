using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class TemplateRepository : ITemplateRepository
    {
        private EFDbContext context = new EFDbContext();

        public Template Get()
        {
            return context.Templates.OrderByDescending(x => x.Id).First();
        }

    }
}
