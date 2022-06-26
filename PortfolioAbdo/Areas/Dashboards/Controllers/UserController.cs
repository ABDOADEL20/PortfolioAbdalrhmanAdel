using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Dashboards")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IApplicationUser user;
        private readonly IToastNotification toastNotification;

        public UserController(UserManager<ApplicationUser> userManager,IMapper mapper,IApplicationUser user, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.user = user;
            this.toastNotification = toastNotification;
        }
        public IEnumerable<ApplicationUserVm> usersVmAll()
        {
            //var Users = userManager.GetUserId(User); // get user Id
            var model = user.Get();
            return mapper.Map<IEnumerable<ApplicationUserVm>>(model);
        }
        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = user.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IActionResult Index()
        {
            MultipleModels models = new MultipleModels();
            //models.ApplicationUserVm = userVm();
            models.ApplicationUserVmAll = usersVmAll();
            ViewData["Message"] = userVm();
            return View(models);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var data = mapper.Map<ApplicationUserVm>(user.GetByID(id));
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(model.Id);

                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("User Deleted successfully");
                        return RedirectToAction("Index", "User", new { area = "Dashboards" });
                    }
                    else
                    {
                        toastNotification.AddErrorToastMessage("Error !!!!,Or Check Testimonials Table");
                        return RedirectToAction("Index", "User", new { area = "Dashboards" });
                    }
                }
                toastNotification.AddErrorToastMessage("Error !!!!,Or Check Testimonials Table");
                return RedirectToAction("Index", "User", new { area = "Dashboards" });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage("Error !!!!,Or Check Testimonials Table");
                return RedirectToAction("Index", "User", new { area = "Dashboards" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            return View(data);
        }
    }
}
