using ProjectPortfolioRiskManager.Domain.Concrete;

namespace ProjectPortfolioRiskManager.WebUI.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserViewModel()
        { }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.UserName;
            Email = user.Email;
        }
    }
}