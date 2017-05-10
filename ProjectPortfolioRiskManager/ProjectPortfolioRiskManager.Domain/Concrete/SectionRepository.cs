using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class SectionRepository : ISectionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Section> GetByTemplate(int templateId)
        {
            return context.Templates.Single(x => x.Id == templateId).Sections;
        }
    }
}
