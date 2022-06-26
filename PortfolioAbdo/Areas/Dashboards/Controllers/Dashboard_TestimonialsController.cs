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
    public class Dashboard_TestimonialsController : Controller
    {
        private readonly ITestimonials testimonials;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public Dashboard_TestimonialsController(ITestimonials testimonials,UserManager<ApplicationUser> userManager, IApplicationUser applicationUser, IMapper mapper, IToastNotification toastNotification)
        {
            this.testimonials = testimonials;
            this.userManager = userManager;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }
        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = applicationUser.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IEnumerable<TestimonialsVm> testimonialsVms()
        {
            return mapper.Map<IEnumerable<TestimonialsVm>>(testimonials.Get());
        }
        public IActionResult Index()
        {
            MultipleModels models = new MultipleModels();
            //models.ApplicationUserVm = userVm();
            models.TestimonialsVmAll = testimonialsVms();
            ViewData["Message"] = userVm();
            return View(models);
        }
        public IActionResult Details(int id)
        {
            var data = mapper.Map<TestimonialsVm>(testimonials.GetByID(id));
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var data = mapper.Map<TestimonialsVm>(testimonials.GetByID(id));
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var data = testimonials.GetByID(id);
            testimonials.Delete(data);
            toastNotification.AddSuccessToastMessage("Deleted Testimonial successfully");
            return RedirectToAction("Index", "Dashboard_Testimonials", new { Area = "Dashboards" });
        }
    }
}
