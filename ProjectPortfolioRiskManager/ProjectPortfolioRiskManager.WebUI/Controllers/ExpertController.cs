using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize(Roles = "Expert, Administrator")]
    public class ExpertController : Controller
    {
        // GET: Expert
        public ActionResult Index()
        {
            return View();
        }
    }
}