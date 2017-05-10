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
        public int? CompanySizeId { get; set; }
        public IEnumerable<string> CompanySizes { get; set; }
        public int? PositionId { get; set; }
        public IEnumerable<string> Positions { get; set; }
        public string Industry { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<LikertItem> LikertItems { get; set; }
        public Dictionary<int, int> Answers { get; set; }

        public QuestionnaireViewModel()
        { }

        public QuestionnaireViewModel(Questionnaire questionnaire)
        {
            Id = questionnaire.Id;
            Content = questionnaire.Template.Content;
            CompanySizeId = questionnaire.CompanySizeId;
            CompanySizes = questionnaire.Template.CompanySizes.Select(x => x.Value);
            PositionId = questionnaire.PositionId;
            Positions = questionnaire.Template.Positions.Select(x => x.Value);
            Industry = questionnaire.Industry;
            Sections = questionnaire.Template.Sections;
            LikertItems = questionnaire.Template.LikertItems.OrderBy(x => x.OrderNumber).Select(x => x.LikertItem);
            Answers = new Dictionary<int, int>();
            foreach (var answer in questionnaire.Answers)
            {
                Answers.Add(answer.QuestionId, answer.LikertItemId);
            }
        }

        public QuestionnaireViewModel(Template template)
        {
            TemplateId = template.Id;
            //Content = template.Content;
            //CompanySizes = template.CompanySizes.Select(x => x.Value);
            //Positions = template.Positions.Select(x => x.Value);
            //Sections = template.Sections;
            //LikertItems = template.LikertItems.OrderBy(x => x.OrderNumber).Select(x => x.LikertItem);
        }
    }
}