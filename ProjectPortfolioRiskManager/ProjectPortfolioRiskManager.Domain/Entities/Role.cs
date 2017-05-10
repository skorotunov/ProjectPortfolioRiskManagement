using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectPortfolioRiskManager.Domain.Entities
{
    public class Role : IdentityRole
    {
        public Role()
            : base() { }

        public Role(string name)
            : base(name) { }
    }
}
