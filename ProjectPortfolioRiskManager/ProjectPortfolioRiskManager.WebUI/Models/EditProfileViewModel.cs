using ProjectPortfolioRiskManager.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class EditProfileViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        public EditProfileViewModel()
        { }

        public EditProfileViewModel(User user)
        {
            Email = user.Email;
            Name = user.UserName;
        }
    }
}