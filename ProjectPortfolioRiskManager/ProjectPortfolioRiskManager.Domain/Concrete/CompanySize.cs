using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class CompanySize
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
    }
}
