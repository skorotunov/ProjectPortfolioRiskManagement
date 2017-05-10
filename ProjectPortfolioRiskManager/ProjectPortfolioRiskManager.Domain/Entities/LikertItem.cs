using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.Domain.Entities
{
    public class LikertItem
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }

        public virtual ICollection<Templates_LikertItems> Templates { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public LikertItem()
        {
            Templates = new HashSet<Templates_LikertItems>();
        }
    }
}
