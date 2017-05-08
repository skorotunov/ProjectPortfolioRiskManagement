using ProjectPortfolioRiskManager.Domain.Concrete;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }        

        public EditUserViewModel()
        { }

        public EditUserViewModel(User user)
        {
            Id = user.Id;
            Name = user.UserName;
            Email = user.Email;
        }
    }
}