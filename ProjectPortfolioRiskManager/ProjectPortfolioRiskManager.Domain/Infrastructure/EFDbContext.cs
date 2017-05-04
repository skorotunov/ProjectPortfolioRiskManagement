using Microsoft.AspNet.Identity.EntityFramework;
using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Migrations;
using System.Data.Entity;

namespace ProjectPortfolioRiskManager.Domain.Infrastructure
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public EFDbContext()
            : base("EFDbContext")
        { }
        
        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new IdentityDbInit());
        }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }
    }

    public class IdentityDbInit : MigrateDatabaseToLatestVersion<EFDbContext, Configuration>
    { }
}
