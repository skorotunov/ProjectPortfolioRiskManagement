using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class QuestionRepository : IQuestionRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Question> GetByTemplate(int templateId)
        {
            var sectionIDs = context.Templates.Single(x => x.Id == templateId).Sections.Select(x => x.Id);
            return context.Questions.Where(x => sectionIDs.Contains(x.SectionId)).OrderBy(x => x.SectionId).ThenBy(x => x.Id);
        }

        public void UpdateValues(int templateId, List<string> list)
        {
            List<Question> currents = GetByTemplate(templateId).ToList();
            for (int i = 0; i < currents.Count(); i++)
            {
                if (list.ElementAtOrDefault(i) != null && !currents[i].Value.Equals(list[i]))
                {
                    currents[i].Value = list[i];
                }
            }
            context.SaveChanges();
        }

        public void InsertValues(int templateId, List<string> list, List<int> sectionsMapping)
        {
            List<int> sectionIDs = context.Templates.Single(x => x.Id == templateId).Sections.Select(x => x.Id).ToList();
            for (int i = 0; i < sectionsMapping.Count(); i++)
            {
                for (int j = 0; j < sectionsMapping[i]; j++)
                {
                    context.Questions.Add(new Question()
                    {
                        SectionId = sectionIDs[i],
                        Value = list[j]
                    });
                }
                list.RemoveRange(0, sectionsMapping[i]);
            }
            context.SaveChanges();
        }
    }
}
