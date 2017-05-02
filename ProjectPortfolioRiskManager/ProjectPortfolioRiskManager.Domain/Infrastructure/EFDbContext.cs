using Microsoft.AspNet.Identity.EntityFramework;
using ProjectPortfolioRiskManager.Domain.Concrete;
using System.Data.Entity;

namespace ProjectPortfolioRiskManager.Domain.Infrastructure
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public EFDbContext() : base("EFDbContext") { }

        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new IdentityDbInit());
        }
        public static EFDbContext Create()
        {
            return new EFDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(EFDbContext context)
        {
            // initial configuration will go here
        }
    }
}
