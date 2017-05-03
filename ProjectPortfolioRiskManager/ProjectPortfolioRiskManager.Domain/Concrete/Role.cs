using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectPortfolioRiskManager.Domain.Concrete
{
    public class Role : IdentityRole
    {
        public Role()
            : base() { }

        public Role(string name)
            : base(name) { }
    }
}
