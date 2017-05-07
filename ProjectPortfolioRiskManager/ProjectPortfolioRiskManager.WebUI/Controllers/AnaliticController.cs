using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Controllers
{
    [Authorize(Roles = "Analytic, Administrator")]
    public class AnaliticController : Controller
    {
        // GET: Analitic
        public ActionResult Index()
        {
            return View();
        }
    }
}