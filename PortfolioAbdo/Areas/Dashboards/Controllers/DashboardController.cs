using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        #region HomeDashboard
        public IActionResult HomeDashboard()
        {
            return View();
        }
        #endregion
    }
}
