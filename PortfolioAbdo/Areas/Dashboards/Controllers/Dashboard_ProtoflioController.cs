using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.DataBase;
using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    public class Dashboard_ProtoflioController : Controller
    {
        private readonly PortfolioContext context;
        private readonly IPortfolio portoflio;
        private readonly ICategory_Portoflio category_Portoflio;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long maxAllowedPosterSize = 1048576;

        public Dashboard_ProtoflioController(PortfolioContext context, IPortfolio _protoflio,ICategory_Portoflio category_Portoflio, IMapper mapper, IToastNotification toastNotification)
        {
            this.context = context;
            portoflio = _protoflio;
            this.category_Portoflio = category_Portoflio;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var data = mapper.Map<IEnumerable<PortfolioVm>>(portoflio.Get());
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var catgportfolioModel = mapper.Map<IEnumerable<Category_PortoflioVm>>(category_Portoflio.Get());
            ViewBag.catgportfolio = new SelectList(catgportfolioModel, "Id", "Category_Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioVm model)
        {
          try { 
            if (!ModelState.IsValid)
            {
                var catgportfolioModel = mapper.Map<IEnumerable<Category_PortoflioVm>>(category_Portoflio.Get());
                ViewBag.catgportfolio = new SelectList(catgportfolioModel, "Id", "Category_Name");
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
            }
                Microsoft.AspNetCore.Http.IFormFileCollection files = Request.Form.Files;

            if (!files.Any())
            {
                ModelState.AddModelError("Project_Photo", "Please select project image!");
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
            }

            var poster = files.FirstOrDefault();

            if (!_allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                ModelState.AddModelError("Project_Photo", "Only .PNG, .JPG Images are allowed");
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
            }

            if (poster.Length > maxAllowedPosterSize)
            {
                ModelState.AddModelError("Project_Photo", "Image cannot br more than 1 MB!");
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
            }

            using var datastream = new MemoryStream();

            await poster.CopyToAsync(datastream);

            //model.Project_Photo = datastream.ToArray();

                var portfolios = new Portfolio
                {
                    Project_Name = model.Project_Name,
                    Project_Description = model.Project_Description,
                    Project_Service = model.Project_Service,
                    Project_Client = model.Project_Client,
                    Project_Date = model.Project_Date,
                    Project_GitHub = model.Project_GitHub,
                    Category_ProtoflioId = model.Category_ProtoflioId,
                    Project_Photo = datastream.ToArray(),
                };
                context.Portfolio.Add(portfolios);
                context.SaveChanges();

                toastNotification.AddSuccessToastMessage("Portoflio item Created successfully");
            return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_Protoflio", new { Area = "Dashboards" }) });
            }
          catch (Exception)
          {
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
          }
        }

        public IActionResult Update()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
