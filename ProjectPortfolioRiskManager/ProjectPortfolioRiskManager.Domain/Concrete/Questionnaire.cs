using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class Questionnaire
    {
        [Key]
        public int Id { get; set; }
        public int TemplateId { get; set; }
        public int CompanySizeId { get; set; }
        public int PositionId { get; set; }
        public string Industry { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        public virtual Template Template { get; set; }
        public virtual CompanySize CompanySize { get; set; }
        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
