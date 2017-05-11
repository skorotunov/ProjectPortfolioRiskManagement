using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class TemplateRepository : ITemplateRepository
    {
        private EFDbContext context = new EFDbContext();

        public Template GetCurrentTemplate()
        {
            return context.Templates.OrderByDescending(x => x.Id).First();
        }

        public int Insert(string content, string userId)
        {
            var now = DateTime.Now;
            var template = new Template()
            {
                Content = content,
                CreationDate = now,
                CreationUserId = userId,
                LastUpdateDate = now,
                LastUpdateUserId = userId
            };
            context.Templates.Add(template);
            context.SaveChanges();
            return template.Id;
        }

        public void UpdateContent(int templateId, string content, string userId)
        {
            var template = context.Templates.Single(x => x.Id == templateId);
            template.Content = content;
            template.LastUpdateDate = DateTime.Now;
            template.LastUpdateUserId = userId;
            context.SaveChanges();
        }
    }
}
