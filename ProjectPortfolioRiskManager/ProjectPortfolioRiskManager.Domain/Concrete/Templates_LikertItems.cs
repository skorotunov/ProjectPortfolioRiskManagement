using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class Templates_LikertItems
    {
        [Key, Column(Order = 0), ForeignKey("Template")]
        public int TemplateRefId { get; set; }
        [Key, Column(Order = 1), ForeignKey("LikertItem")]
        public int LikertItemRefId { get; set; }
        public int OrderNumber { get; set; }

        public virtual Template Template { get; set; }
        public virtual LikertItem LikertItem { get; set; }
    }
}
