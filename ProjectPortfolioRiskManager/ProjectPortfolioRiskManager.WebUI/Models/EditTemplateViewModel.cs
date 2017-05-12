using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.WebUI.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class EditTemplateViewModel
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [AllowHtml]
        public string Content { get; set; }
        public bool IsInsertNew { get; set; }

        public EditTemplateViewModel()
        { }

        public EditTemplateViewModel(Func<string, object, string> renderPartialView, ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository,
            IPositionRepository positionRepository, ISectionRepository sectionRepository, IQuestionRepository questionRepository, ILikertItemRepository likertItemRepository)
        {
            var currentTemplate = templateRepository.GetCurrentTemplate();
            Id = currentTemplate.Id;
            Content = TemplateLogic.GetFullContent(currentTemplate.Id, currentTemplate.Content, renderPartialView, companySizeRepository, positionRepository, sectionRepository,
                questionRepository, likertItemRepository);
        }

        public void Submit(string userId, ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository, IPositionRepository positionRepository,
            ISectionRepository sectionRepository, IQuestionRepository questionRepository, ILikertItemRepository likertItemRepository)
        {
            List<string> companySizes, positions, sections, questions, likertItems;
            List<int> questionsToSectionsMapping;
            var minimizedContent = TemplateLogic.ProcessContent(Content, IsInsertNew, out companySizes, out positions, out sections, out questions, out likertItems, out questionsToSectionsMapping);
            if (IsInsertNew)
            {
                Id = templateRepository.Insert(minimizedContent, userId);
                companySizeRepository.InsertValues(Id, companySizes);
                positionRepository.InsertValues(Id, positions);
                sectionRepository.InsertValues(Id, sections);
                questionRepository.InsertValues(Id, questions, questionsToSectionsMapping);
                likertItemRepository.InsertValues(Id, likertItems);
                IsInsertNew = false;
            }
            else
            {
                templateRepository.UpdateContent(Id, minimizedContent, userId);
                companySizeRepository.UpdateValues(Id, companySizes);
                positionRepository.UpdateValues(Id, positions);
                sectionRepository.UpdateValues(Id, sections);
                questionRepository.UpdateValues(Id, questions);
                likertItemRepository.UpdateValues(Id, likertItems);
            }
        }
    }
}