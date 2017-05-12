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
            return context.Templates.Single(x => x.Id == templateId).Sections.OrderBy(x => x.Id);
        }

        public void UpdateValues(int templateId, List<string> list)
        {
            List<Section> currents = context.Templates.Single(x => x.Id == templateId).Sections.OrderBy(x => x.Id).ToList();
            for (int i = 0; i < currents.Count(); i++)
            {
                if (list.ElementAtOrDefault(i) != null && !currents[i].Name.Equals(list[i]))
                {
                    currents[i].Name = list[i];
                }
            }
            context.SaveChanges();
        }

        public void InsertValues(int templateId, List<string> list)
        {
            ICollection<Section> current = context.Templates.Single(x => x.Id == templateId).Sections;
            foreach (var item in list)
            {
                current.Add(new Section()
                {
                    Name = item
                });
            }
            context.SaveChanges();
        }
    }
}
