using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private readonly IHome home;
        private readonly IMapper mapper;

        public HomeController(IHome _home, IMapper mapper)
        {
            home = _home;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        public FileResult DownloadFile()
        {
            var data = mapper.Map<HomeVm>(home.Get().FirstOrDefault());

            return File(data.Cv, "application/pdf","Cv Abdalrhman Adel.pdf");
        }

    }
}
