using ProjectPortfolioRiskManager.Domain.Entities;

namespace ProjectPortfolioRiskManager.Domain.Abstract
{
    public interface ITemplateRepository
    {
        Template GetCurrentTemplate();
        int Insert(string content, string userId);
        void UpdateContent(int templateId, string content, string userId);
    }
}
