using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class QuestionnaireViewModel
    {
        public int? Id { get; set; }
        public int TemplateId { get; set; }
        public string Content { get; set; }
        public int CompanySizeId { get; set; }
        public int PositionId { get; set; }
        public string Industry { get; set; }
        public Dictionary<string, int?> Answers { get; set; }

        public QuestionnaireViewModel()
        { }

        public QuestionnaireViewModel(string userId, IQuestionnaireRepository questionnaireRepository, ITemplateRepository templateRepository, ICompanySizeRepository companySizeRepository,
            IQuestionRepository questionRepository)
        {
            IEnumerable<Questionnaire> questionnaires = questionnaireRepository.GetByUser(userId);
            Template currentTemplate = templateRepository.GetCurrentTemplate();
            Questionnaire questionnaire = questionnaires.SingleOrDefault(x => x.TemplateId == currentTemplate.Id);
            if (questionnaire != null)
            {
                Id = questionnaire.Id;
                CompanySizeId = questionnaire.CompanySizeId;
                PositionId = questionnaire.PositionId;
                Industry = questionnaire.Industry;
                List<Question> questions = questionRepository.GetByTemplate(currentTemplate.Id).ToList();
                Answers = new Dictionary<string, int?>();
                for (int i = 0; i < questions.Count(); i++)
                {
                    Answers.Add(i.ToString(), questions[i].Answers.SingleOrDefault(x => x.QuestionnaireId == Id)?.LikertItemId);
                }
            }
            TemplateId = currentTemplate.Id;
            Content = currentTemplate.Content;
        }

        public void Submit(int templateId, int companySizeId, int positionId, string industry, Dictionary<string, int?> answers, string userId, int? id, IQuestionnaireRepository questionnaireRepository)
        {
            questionnaireRepository.Save(templateId, companySizeId, positionId, industry, answers, userId, id);
        }
    }
}