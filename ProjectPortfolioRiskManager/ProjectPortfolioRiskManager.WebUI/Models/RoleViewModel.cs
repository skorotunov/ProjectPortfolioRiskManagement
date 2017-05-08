using Microsoft.AspNet.Identity.EntityFramework;
using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> UserNames { get; set; }

        public RoleViewModel()
        { }

        public RoleViewModel(Role role, EFUserManager userManager)
        {
            Id = role.Id;
            Name = role.Name;
            UserNames = new List<string>();
            foreach (IdentityUserRole userRole in role.Users)
            {
                var name = userManager.FindByIdAsync(userRole.UserId).Result.UserName;
                UserNames.Add(name);
            }
        }
    }
}