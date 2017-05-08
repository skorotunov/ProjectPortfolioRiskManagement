using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public int SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
