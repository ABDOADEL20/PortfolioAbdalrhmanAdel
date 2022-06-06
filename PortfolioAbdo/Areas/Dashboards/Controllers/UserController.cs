﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            return View();
        }
    }
}
