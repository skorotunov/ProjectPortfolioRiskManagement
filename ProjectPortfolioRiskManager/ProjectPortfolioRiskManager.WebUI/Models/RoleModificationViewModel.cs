using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class RoleModificationViewModel
    {
        [Required]
        public string Name { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}