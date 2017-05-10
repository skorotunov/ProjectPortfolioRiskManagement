using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProjectPortfolioRiskManager.Domain.Entities;
using System;

namespace ProjectPortfolioRiskManager.Domain.Infrastructure
{
    public class EFRoleManager : RoleManager<Role>, IDisposable
    {
        public EFRoleManager(RoleStore<Role> store)
            : base(store) { }

        public static EFRoleManager Create(IdentityFactoryOptions<EFRoleManager> options, IOwinContext context)
        {
            return new EFRoleManager(new RoleStore<Role>(context.Get<EFDbContext>()));
        }
    }
}
