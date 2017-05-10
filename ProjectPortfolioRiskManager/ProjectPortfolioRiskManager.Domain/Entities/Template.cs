using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.Domain.Entities
{
    public class Template
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public string CreationUserId { get; set; }
        public string LastUpdateUserId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual User CreationUser { get; set; }
        public virtual User LastUpdateUser { get; set; }

        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
        public virtual ICollection<CompanySize> CompanySizes { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Templates_LikertItems> LikertItems { get; set; }
        public virtual ICollection<Section> Sections { get; set; }

        public Template()
        {
            LikertItems = new HashSet<Templates_LikertItems>();
        }
    }
}
