using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Infrastructure;
using ProjectPortfolioRiskManager.WebUI.BLL;
using System.Collections.Generic;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class EditRoleViewModel
    {
        public string Name { get; set; }
        public Dictionary<string, string> Members { get; set; }
        public Dictionary<string, string> NonMembers { get; set; }

        public EditRoleViewModel()
        { }

        public EditRoleViewModel(Role role, EFUserManager userManager, EFRoleManager roleManager)
        {
            Name = role.Name;
            Members = new Dictionary<string, string>();
            IEnumerable<User> members = RoleLogic.GetRoleMembers(userManager, roleManager, Name);
            foreach (var member in members)
            {
                Members.Add(member.Id, member.UserName);
            }
            NonMembers = new Dictionary<string, string>();
            IEnumerable<User> nonMembers = RoleLogic.GetNonRoleMembers(userManager, roleManager, Name);
            foreach (var nonMember in nonMembers)
            {
                NonMembers.Add(nonMember.Id, nonMember.UserName);
            }
        }
    }
}