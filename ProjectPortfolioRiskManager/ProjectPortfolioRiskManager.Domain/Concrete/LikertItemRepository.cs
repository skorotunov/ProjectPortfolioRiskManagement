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

        public void UpdateValues(int templateId, List<string> list)
        {
            List<LikertItem> currents = GetByTemplate(templateId).ToList();
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
            int i = 0;
            ICollection<Templates_LikertItems> current = context.Templates.Single(x => x.Id == templateId).LikertItems;
            foreach (var item in list)
            {
                current.Add(new Templates_LikertItems()
                {
                    TemplateRefId = templateId,
                    LikertItem = new LikertItem()
                    {
                        Value = item
                    },
                    OrderNumber = i++
                });
            }
            context.SaveChanges();
        }
    }
}
