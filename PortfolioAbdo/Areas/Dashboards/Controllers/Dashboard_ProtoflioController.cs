using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.DataBase;
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
    public class Dashboard_ProtoflioController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUser applicationUser;
        private readonly PortfolioContext context;
        private readonly IPortfolio portoflio;
        private readonly ICategory_Portoflio category_Portoflio;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long maxAllowedPosterSize = 1048576;

        public Dashboard_ProtoflioController(UserManager<ApplicationUser> userManager, IApplicationUser applicationUser, PortfolioContext context, IPortfolio _protoflio,ICategory_Portoflio category_Portoflio, IMapper mapper, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.applicationUser = applicationUser;
            this.context = context;
            portoflio = _protoflio;
            this.category_Portoflio = category_Portoflio;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = applicationUser.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IEnumerable<PortfolioVm> portfolioVms()
        {
            return mapper.Map<IEnumerable<PortfolioVm>>(portoflio.Get());
        }
        [HttpGet]
        public IActionResult Index()
        {
            //var data = mapper.Map<IEnumerable<PortfolioVm>>(portoflio.Get());
            MultipleModels models = new MultipleModels();
            //models.ApplicationUserVm = userVm();
            models.PortfolioVmAll = portfolioVms();
            ViewData["Message"] = userVm();
            return View(models);
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
        public IActionResult Create(PortfolioVm model)
        { 
                if (ModelState.IsValid)
                {
                    string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/");
                    // 2) Get File Name
                    string FileName = Guid.NewGuid() + Path.GetFileName(model.Project_Photo.FileName);

                    // 3) Merge Physical Path + File Name
                    string FinalPath = Path.Combine(PhysicalPath, FileName);

                    // 4) Save The File As Streams "Data Over Time"
                    using (var stream = new FileStream(FinalPath, FileMode.Create))
                    {
                        model.Project_Photo.CopyTo(stream);
                    }

                    model.Project_Photo_Name = FileName;
                    var obj = mapper.Map<Portfolio>(model);
                    portoflio.Create(obj);

                    toastNotification.AddSuccessToastMessage("Portoflio item Created successfully");
                    return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_Protoflio", new { Area = "Dashboards" }) });
                }
                var catgportfolioModel = mapper.Map<IEnumerable<Category_PortoflioVm>>(category_Portoflio.Get());
                ViewBag.catgportfolio = new SelectList(catgportfolioModel, "Id", "Category_Name");
                return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var catgportfolioModel = mapper.Map<IEnumerable<Category_PortoflioVm>>(category_Portoflio.Get());
            ViewBag.catgportfolio = new SelectList(catgportfolioModel, "Id", "Category_Name");
            var data = mapper.Map<PortfolioVm>(portoflio.GetByID(id));
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(PortfolioVm model,Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                var old2 = context.Portfolio.Where(a => a.Id == portfolio.Id).AsNoTracking().FirstOrDefault();
                string oldfilename = old2.Project_Photo_Name;
                if(model.Project_Photo == null)
                {
                    model.Project_Photo_Name = oldfilename;
                }

                if (model.Project_Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles", oldfilename)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles", oldfilename));
                    string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/");
                    // 2) Get File Name
                    string FileName = Guid.NewGuid() + Path.GetFileName(model.Project_Photo.FileName);

                    // 3) Merge Physical Path + File Name
                    string FinalPath = Path.Combine(PhysicalPath, FileName);

                    // 4) Save The File As Streams "Data Over Time"
                    using (var stream = new FileStream(FinalPath, FileMode.Create))
                    {
                        model.Project_Photo.CopyTo(stream);
                    }

                    model.Project_Photo_Name = FileName;
                }
                var obj = mapper.Map<Portfolio>(model);
                portoflio.Update(obj);

                toastNotification.AddSuccessToastMessage("Portoflio item Updated successfully");
                return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_Protoflio", new { Area = "Dashboards" }) });
            }
            var catgportfolioModel = mapper.Map<IEnumerable<Category_PortoflioVm>>(category_Portoflio.Get());
            ViewBag.catgportfolio = new SelectList(catgportfolioModel, "Id", "Category_Name");
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Update", model) });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = mapper.Map<PortfolioVm>(portoflio.GetByID(id));
            return View(data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = mapper.Map<PortfolioVm>(portoflio.GetByID(id));
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var data = portoflio.GetByID(id);
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles", data.Project_Photo_Name)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles", data.Project_Photo_Name));
                }
                portoflio.Delete(data);
                toastNotification.AddSuccessToastMessage("Portoflio item Deleted successfully");
                return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_Protoflio", new { Area = "Dashboards" }) });
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
