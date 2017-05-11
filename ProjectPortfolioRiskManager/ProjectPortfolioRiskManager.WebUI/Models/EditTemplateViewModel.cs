using ProjectPortfolioRiskManager.Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class EditTemplateViewModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required]
        public string Content { get; set; }
        public bool IsInsertNew { get; set; }

        public EditTemplateViewModel()
        { }

        public EditTemplateViewModel(ITemplateRepository templateRepository)
        {
            var currentTemplate = templateRepository.GetCurrentTemplate();
            Id = currentTemplate.Id;
            Content = currentTemplate.Content; ;
        }

        public void Submit(ITemplateRepository templateRepository, string userId)
        {
            if (IsInsertNew)
            {
                Id = templateRepository.Insert(Content, userId);
                IsInsertNew = false;
            }
            else
            {
                templateRepository.UpdateContent(Id, Content, userId);
            }
            //todo: update additional lists
        }
    }
}