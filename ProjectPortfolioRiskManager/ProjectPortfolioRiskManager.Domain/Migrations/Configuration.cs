using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Data.Entity.Migrations;

namespace ProjectPortfolioRiskManager.Domain.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EFDbContext context)
        {
            var userMgr = new EFUserManager(new UserStore<User>(context));
            var roleMgr = new EFRoleManager(new RoleStore<Role>(context));
            var adminRoleName = "Administrator";
            var expertRoleName = "Expert";
            var analyticRoleName = "Analytic";
            var adminName = "admin";
            var password = "adm1n";
            var email = "serij007@gmail.com";
            if (!roleMgr.RoleExists(adminRoleName))
            {
                roleMgr.Create(new Role(adminRoleName));
            }
            if (!roleMgr.RoleExists(expertRoleName))
            {
                roleMgr.Create(new Role(expertRoleName));
            }
            if (!roleMgr.RoleExists(analyticRoleName))
            {
                roleMgr.Create(new Role(analyticRoleName));
            }
            User admin = userMgr.FindByName(adminName);
            if (admin == null)
            {
                userMgr.Create(new User { UserName = adminName, Email = email }, password);
                admin = userMgr.FindByName(adminName);
            }
            if (!userMgr.IsInRole(admin.Id, adminRoleName))
            {
                userMgr.AddToRole(admin.Id, adminRoleName);
            }
            context.SaveChanges();
        }
    }
}
