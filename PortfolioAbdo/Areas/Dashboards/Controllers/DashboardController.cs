using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.Entity;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUser applicationUser;
        private readonly IExpCompaniesPhoto photo;
        private readonly IHome home;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private new List<string> _allowedExtenstions = new List<string> { ".pdf" };
        private long maxAllowedPosterSizeCV = 10485760;

        public DashboardController(UserManager<ApplicationUser> userManager,IApplicationUser applicationUser ,IExpCompaniesPhoto photo ,IHome _home, IMapper mapper, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.applicationUser = applicationUser;
            this.photo = photo;
            home = _home;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = applicationUser.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IActionResult Index()
        {
            MultipleModels models = new MultipleModels();
            models.ApplicationUserVm = userVm();
            ViewData["Message"] = userVm();
            return View(models);
        }
        #region HomeDashboard

        [HttpGet]
        public IActionResult HomeDashboard()
        {
            MultipleModels models = new MultipleModels();
            models.HomeVm = Homes();
            models.ExpCompaniesPhotoVm = ExpCompanies();
            //models.ApplicationUserVm = userVm();
            ViewData["Message"] = userVm();
            //var data = mapper.Map<HomeVm>(home.Get().FirstOrDefault());
            return View(models);
        }
        public HomeVm Homes()
        {
            return mapper.Map<HomeVm>(home.Get().FirstOrDefault());
        }
        public Homes SetHomes(MultipleModels models)
        {
            return mapper.Map<Homes>(models.HomeVm);
        }
        public IEnumerable<ExpCompaniesPhotoVm> ExpCompanies()
        {
            return mapper.Map<IEnumerable<ExpCompaniesPhotoVm>>(photo.Get());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEditInformation(MultipleModels models)
        {
            if (models.HomeVm.Id == 0 || models.HomeVm.Id==null) 
            { 
                    var files = Request.Form.Files;

                    if (!files.Any())
                    {
                           models.HomeVm = Homes();
                           models.ExpCompaniesPhotoVm = ExpCompanies();
                           ModelState.AddModelError("Cv", "Please select CV file!");
                           return View("HomeDashboard", models);
                    }

                    var cvfile = files.FirstOrDefault();

                    if (!_allowedExtenstions.Contains(Path.GetExtension(cvfile.FileName).ToLower()))
                    {
                        ModelState.AddModelError("Cv", "Only .pdf Files are allowed");
                        models.HomeVm = Homes();
                        models.ExpCompaniesPhotoVm = ExpCompanies();
                        return View("HomeDashboard", models);
                    }

                    if (cvfile.Length > maxAllowedPosterSizeCV)
                    {
                        models.HomeVm = Homes();
                        models.ExpCompaniesPhotoVm = ExpCompanies();
                        ModelState.AddModelError("Cv", "CV cannot br more than 10 MB!");
                        return View("HomeDashboard", models);
                    }

                    using var datastream = new MemoryStream();

                    await cvfile.CopyToAsync(datastream);

                    models.HomeVm.Cv = datastream.ToArray();
                    
                    var obj = SetHomes(models);
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

                    models.HomeVm.Cv = dataStream.ToArray();

                    if (!_allowedExtenstions.Contains(Path.GetExtension(cvfile.FileName).ToLower()))
                    {
                        models.HomeVm = Homes();
                        models.ExpCompaniesPhotoVm = ExpCompanies();
                        ModelState.AddModelError("Cv", "Only .pdf Files are allowed");
                        return View("HomeDashboard", models);
                    }

                    if (cvfile.Length > maxAllowedPosterSizeCV)
                    {
                        models.HomeVm = Homes();
                        models.ExpCompaniesPhotoVm = ExpCompanies();
                        ModelState.AddModelError("Cv", "CV cannot br more than 10 MB!");
                        return View("HomeDashboard", models);
                    }

                }

                var obj = SetHomes(models);
                home.Update(obj);
                toastNotification.AddSuccessToastMessage("Info Updated successfully");
                return RedirectToAction(nameof(HomeDashboard));
            }
        }

        public ExpCompaniesPhoto Photos(MultipleModels model)
        {
            return mapper.Map<ExpCompaniesPhoto>(model.ExpCompaniesPhotoVmModel);
        }

        #endregion
        [HttpPost]
        public IActionResult SetPhotoExp(MultipleModels model)
        {
            if (ModelState.IsValid)
            {
                string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/PhotoExp/");
                // 2) Get File Name
                string FileName = Guid.NewGuid() + Path.GetFileName(model.ExpCompaniesPhotoVmModel.Image.FileName);

                // 3) Merge Physical Path + File Name
                string FinalPath = Path.Combine(PhysicalPath, FileName);

                // 4) Save The File As Streams "Data Over Time"
                using (var stream = new FileStream(FinalPath, FileMode.Create))
                {
                    model.ExpCompaniesPhotoVmModel.Image.CopyTo(stream);
                }

                model.ExpCompaniesPhotoVmModel.ImageName = FileName;
                var obj = Photos(model);
                photo.Create(obj);

                toastNotification.AddSuccessToastMessage("Photo Created successfully");
                return RedirectToAction("HomeDashboard", "Dashboard", new { Area = "Dashboards" });
            }
            model.HomeVm = Homes();
            model.ExpCompaniesPhotoVm = ExpCompanies();
            toastNotification.AddErrorToastMessage("Error !!!");
            return RedirectToAction("HomeDashboard", "Dashboard", new { Area = "Dashboards" });
        }

        [HttpPost]
        public IActionResult DeletePhotoExp(int id)
        {
            try
            {
                var data = photo.GetByID(id);
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles/PhotoExp/", data.ImageName)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles/PhotoExp/", data.ImageName));
                }
                photo.Delete(data);
                toastNotification.AddSuccessToastMessage("Deleted Photo successfully");
                return RedirectToAction("HomeDashboard", "Dashboard", new { Area = "Dashboards" });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage("Error !!!");
                return RedirectToAction("HomeDashboard", "Dashboard", new { Area = "Dashboards" });
            }
        }

    }
}
