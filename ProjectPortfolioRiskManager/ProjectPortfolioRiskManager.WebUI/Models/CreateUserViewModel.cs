using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class CreateUserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }        
    }
}