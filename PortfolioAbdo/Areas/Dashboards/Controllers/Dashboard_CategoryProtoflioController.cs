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
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    [Authorize(Roles = "Admin")]
    public class Dashboard_CategoryProtoflioController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUser applicationUser;
        private readonly ICategory_Portoflio category;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public Dashboard_CategoryProtoflioController(UserManager<ApplicationUser> userManager, IApplicationUser applicationUser, ICategory_Portoflio _category, IMapper mapper, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.applicationUser = applicationUser;
            category = _category;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = applicationUser.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IEnumerable<Category_PortoflioVm> category_PortoflioVms()
        {
            return mapper.Map<IEnumerable<Category_PortoflioVm>>(category.Get());
        }
        public IActionResult Index()
        {
            MultipleModels models = new MultipleModels();
            //var data = mapper.Map<IEnumerable<Category_PortoflioVm>>(category.Get());
            //models.ApplicationUserVm = userVm();
            models.Category_PortoflioVmAll = category_PortoflioVms();
            ViewData["Message"] = userVm();
            return View(models);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category_PortoflioVm model)
        {
            if (ModelState.IsValid)
            {
                var obj = mapper.Map<Category_Portoflio>(model);
                category.Create(obj);

                toastNotification.AddSuccessToastMessage("Category Created successfully");
                return Json(new { isValid = true, newUrl= Url.Action("Index", "Dashboard_CategoryProtoflio", new { Area = "Dashboards" }) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", model) });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = category.GetByID(id);
            var data = mapper.Map<Category_PortoflioVm>(model);
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(Category_PortoflioVm model)
        {
          if (ModelState.IsValid)
          {  
            var data = mapper.Map<Category_Portoflio>(model);
            category.Update(data);
            toastNotification.AddSuccessToastMessage("Category Updated successfully");
            return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_CategoryProtoflio", new { Area = "Dashboards" }) });
          }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Update", model) });
        }

        public IActionResult Details(int id)
        {
            var data = mapper.Map<Category_PortoflioVm>(category.GetByID(id));
            return View(data);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = mapper.Map<Category_PortoflioVm>(category.GetByID(id));
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Category_PortoflioVm model)
        {
            if (ModelState.IsValid)
            {
               
                var data = mapper.Map<Category_Portoflio>(model);
                category.Delete(data);
                toastNotification.AddSuccessToastMessage("Category Deleted successfully");
                return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_CategoryProtoflio", new { Area = "Dashboards" }) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Delete", model) });

        }
    }
}
