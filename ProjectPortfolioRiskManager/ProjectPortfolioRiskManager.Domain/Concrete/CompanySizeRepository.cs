using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class CompanySizeRepository : ICompanySizeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<CompanySize> GetByTemplate(int templateId)
        {
            return context.Templates.Single(x => x.Id == templateId).CompanySizes.OrderBy(x => x.Id);
        }

        public void UpdateValues(int templateId, List<string> list)
        {
            List<CompanySize> currents = context.Templates.Single(x => x.Id == templateId).CompanySizes.OrderBy(x => x.Id).ToList();
            for (int i = 0; i < currents.Count(); i++)
            {
                if (list.ElementAtOrDefault(i) != null && !currents[i].Value.Equals(list[i]))
                {
                    currents[i].Value = list[i];
                }
            }
            context.SaveChanges();
        }

        public void InsertValues(int templateId, List<string> list)
        {
            ICollection<CompanySize> current = context.Templates.Single(x => x.Id == templateId).CompanySizes;
            foreach (var item in list)
            {
                current.Add(new CompanySize()
                {
                    Value = item
                });
            }
            context.SaveChanges();
        }
    }
}
