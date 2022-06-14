using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    public class DashboardController : Controller
    {
        private readonly IHome home;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private new List<string> _allowedExtenstions = new List<string> { ".pdf" };
        private long maxAllowedPosterSizeCV = 10485760;

        public DashboardController(IHome _home, IMapper mapper, IToastNotification toastNotification)
        {
            home = _home;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region HomeDashboard

        [HttpGet]
        public IActionResult HomeDashboard()
        {
            var data = mapper.Map<HomeVm>(home.Get().FirstOrDefault());
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEditInformation(HomeVm model)
        {
            if (model.Id == 0 || model.Id==null) 
            { 
                    var files = Request.Form.Files;

                    if (!files.Any())
                    {
                        ModelState.AddModelError("Cv", "Please select CV file!");
                        return View("HomeDashboard", model);
                    }

                    var cvfile = files.FirstOrDefault();

                    if (!_allowedExtenstions.Contains(Path.GetExtension(cvfile.FileName).ToLower()))
                    {
                        ModelState.AddModelError("Cv", "Only .pdf Files are allowed");
                        return View("HomeDashboard", model);
                    }

                    if (cvfile.Length > maxAllowedPosterSizeCV)
                    {
                        ModelState.AddModelError("Cv", "CV cannot br more than 10 MB!");
                        return View("HomeDashboard", model);
                    }

                    using var datastream = new MemoryStream();

                    await cvfile.CopyToAsync(datastream);

                    model.Cv = datastream.ToArray();
                    
                    var obj = mapper.Map<Homes>(model);
                    home.Create(obj);

                    toastNotification.AddSuccessToastMessage("Info Created successfully");
                    return RedirectToAction(nameof(HomeDashboard));
            }
            else
            {

                var files = Request.Form.Files;

                if (files.Any())
                {
                    var cvfile = files.FirstOrDefault();

                    using var dataStream = new MemoryStream();

                    await cvfile.CopyToAsync(dataStream);

                    model.Cv = dataStream.ToArray();

                    if (!_allowedExtenstions.Contains(Path.GetExtension(cvfile.FileName).ToLower()))
                    {
                        ModelState.AddModelError("Cv", "Only .pdf Files are allowed");
                        return View("HomeDashboard", model);
                    }

                    if (cvfile.Length > maxAllowedPosterSizeCV)
                    {
                        ModelState.AddModelError("Cv", "CV cannot br more than 10 MB!");
                        return View("HomeDashboard", model);
                    }

                }

                var obj = mapper.Map<Homes>(model);
                home.Update(obj);
                toastNotification.AddSuccessToastMessage("Info Updated successfully");
                return RedirectToAction(nameof(HomeDashboard));
            }
        }
        #endregion
    }
}
