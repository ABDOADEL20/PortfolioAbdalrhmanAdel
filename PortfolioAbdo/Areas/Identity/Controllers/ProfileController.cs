using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class ProfileController : Controller
    {
        public IActionResult MyProfile(string id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditInformation()
        {
            return View();
        }

        public IActionResult UpdateTestim()
        {
            return View();
        }

       
        public IActionResult DetailsTestim()
        {
            return View();
        }

       
        public IActionResult DeleteTestim()
        {
            return View();
        }
    }
}
