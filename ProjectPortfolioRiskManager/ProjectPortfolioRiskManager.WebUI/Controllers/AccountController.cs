using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
    }
}