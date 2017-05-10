using Microsoft.AspNet.Identity;
using ProjectPortfolioRiskManager.Domain.Entities;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace ProjectPortfolioRiskManager.WebUI.BLL
{
    public static class RoleLogic
    {
        public static IEnumerable<User> GetRoleMembers(EFUserManager userManager, EFRoleManager roleManager, string roleName)
        {
            Role role = roleManager.FindByName(roleName);
            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<User> members = userManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            return members;
        }

        public static IEnumerable<User> GetNonRoleMembers(EFUserManager userManager, EFRoleManager roleManager, string roleName)
        {
            IEnumerable<User> members = GetRoleMembers(userManager, roleManager, roleName);
            IEnumerable<User> nonMembers = userManager.Users.Except(members);
            return nonMembers;
        }
    }
}