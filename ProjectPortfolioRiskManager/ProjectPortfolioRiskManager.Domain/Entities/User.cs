using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.Domain.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<Questionnaire> Questionnaires { get; set; }
        public virtual ICollection<Template> CreatedTemplates { get; set; }
        public virtual ICollection<Template> UpdatedTemplates { get; set; }
    }
}
