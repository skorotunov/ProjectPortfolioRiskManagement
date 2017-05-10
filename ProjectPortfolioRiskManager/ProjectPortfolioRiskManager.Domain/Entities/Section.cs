using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.Domain.Entities
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TemplateId { get; set; }

        public virtual Template Template { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
