using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Questionnaire> GetByUser(string userId)
        {
            return context.Questionnaires.Where(x => x.UserId.Equals(userId));
        }

        public void Save(int templateId, int companySizeId, int positionId, string industry, Dictionary<string, int?> answers, string userId, int? id)
        {
            Questionnaire questionnaire = null;
            var now = DateTime.Now;
            if (id.HasValue)
            {
                questionnaire = context.Questionnaires.Single(x => x.Id == id.Value);
                var currentAnswers = context.Answers.Where(x => x.QuestionnaireId == id.Value);
                context.Answers.RemoveRange(currentAnswers);
            }
            else
            {
                questionnaire = new Questionnaire();
                context.Questionnaires.Add(questionnaire);
                questionnaire.CreationDate = now;
            }
            questionnaire.TemplateId = templateId;
            questionnaire.CompanySizeId = companySizeId;
            questionnaire.PositionId = positionId;
            questionnaire.Industry = industry;
            questionnaire.UserId = userId;
            questionnaire.LastUpdateDate = now;
            context.SaveChanges();

            IEnumerable<int> sectionIDs = context.Templates.Single(x => x.Id == templateId).Sections.Select(x => x.Id);
            List<Question> questions = context.Questions.Where(x => sectionIDs.Contains(x.SectionId)).OrderBy(x => x.SectionId).ThenBy(x => x.Id).ToList();
            for (int i = 0; i < questions.Count(); i++)
            {
                if (answers.Keys.Contains(i.ToString()))
                {
                    context.Answers.Add(new Answer()
                    {
                        LikertItemId = answers[i.ToString()].Value,
                        QuestionId = questions[i].Id,
                        QuestionnaireId = questionnaire.Id
                    });
                }
            }

            context.SaveChanges();
        }
    }
}
