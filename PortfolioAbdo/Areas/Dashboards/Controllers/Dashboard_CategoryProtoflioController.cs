using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    public class Dashboard_CategoryProtoflioController : Controller
    {
        private readonly ICategory_Portoflio category;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public Dashboard_CategoryProtoflioController(ICategory_Portoflio _category, IMapper mapper, IToastNotification toastNotification)
        {
            category = _category;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            var data = mapper.Map<IEnumerable<Category_PortoflioVm>>(category.Get());
            return View(data);
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
                model.Is_Deleted = true;
                var data = mapper.Map<Category_Portoflio>(model);
                category.Update(data);
                toastNotification.AddSuccessToastMessage("Category Deleted successfully");
                return Json(new { isValid = true, newUrl = Url.Action("Index", "Dashboard_CategoryProtoflio", new { Area = "Dashboards" }) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Delete", model) });

        }
    }
}
