using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProjectPortfolioRiskManager.Domain.Entities;

namespace ProjectPortfolioRiskManager.Domain.Infrastructure
{
    public class EFUserManager : UserManager<User>
    {
        public EFUserManager(IUserStore<User> store)
            : base(store) { }

        public static EFUserManager Create(IdentityFactoryOptions<EFUserManager> options, IOwinContext context)
        {
            var db = context.Get<EFDbContext>();
            var manager = new EFUserManager(new UserStore<User>(db));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            manager.UserValidator = new UserValidator<User>(manager)
            {
                RequireUniqueEmail = true
            };

            return manager;
        }
    }
}
