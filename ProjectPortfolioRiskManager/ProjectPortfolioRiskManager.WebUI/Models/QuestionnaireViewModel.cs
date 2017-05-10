using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class QuestionnaireViewModel
    {
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public string Content { get; set; }
        public int CompanySizeId { get; set; }
        public int PositionId { get; set; }
        public string Industry { get; set; }
        public Dictionary<int, int> Answers { get; set; }

        public QuestionnaireViewModel()
        { }

        public QuestionnaireViewModel(string userName, IQuestionnaireRepository questionnaireRepository, ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository)
        {
            IEnumerable<Questionnaire> questionnaires = questionnaireRepository.GetByUser(userName);
            Template currentTemplate = templateRepository.GetCurrentTemplate();
            Questionnaire questionnaire = questionnaires.SingleOrDefault(x => x.TemplateId == currentTemplate.Id);
            if (questionnaire != null)
            {
                Id = questionnaire.Id;
                CompanySizeId = questionnaire.CompanySizeId;
                PositionId = questionnaire.PositionId;
                Industry = questionnaire.Industry;
            }
            TemplateId = currentTemplate.Id;
            Content = currentTemplate.Content;
        }

        public QuestionnaireViewModel(Questionnaire questionnaire)
        {

            Content = questionnaire.Template.Content;

            //

            //Sections = questionnaire.Template.Sections;
            //LikertItems = questionnaire.Template.LikertItems.OrderBy(x => x.OrderNumber).Select(x => x.LikertItem);
            //Answers = new Dictionary<int, int>();
            //foreach (var answer in questionnaire.Answers)
            //{
            //    Answers.Add(answer.QuestionId, answer.LikertItemId);
            //}
        }
    }
}