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
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private readonly IPortfolio portfolio;
        private readonly IExpCompaniesPhoto photo;
        private readonly IToastNotification toastNotification;
        private readonly ITestimonials testimonials;
        private readonly IApplicationUser applicationUser;
        private readonly ICategory_Portoflio category_Portoflio;
        private readonly IHome home;
        private readonly IMapper mapper;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IPortfolio portfolio, IExpCompaniesPhoto photo, IToastNotification toastNotification, ITestimonials testimonials, IApplicationUser applicationUser,
            ICategory_Portoflio category_Portoflio, IHome _home, IMapper mapper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.portfolio = portfolio;
            this.photo = photo;
            this.toastNotification = toastNotification;
            this.testimonials = testimonials;
            this.applicationUser = applicationUser;
            this.category_Portoflio = category_Portoflio;
            home = _home;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = applicationUser.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IEnumerable<TestimonialsVm> TestimonialsVms()
        {
            return mapper.Map<IEnumerable<TestimonialsVm>>(testimonials.Get());
        }
        public IEnumerable<HomeVm> GetHome()
        {
            return mapper.Map<IEnumerable<HomeVm>>(home.Get());
        }
        public Testimonials TestimonialFunction(MultipleModels model)
        {
            return mapper.Map<Testimonials>(model.TestimonialsVm);
        }
        public IEnumerable<ExpCompaniesPhotoVm> GetPhotosExp()
        {
            return mapper.Map<IEnumerable<ExpCompaniesPhotoVm>>(photo.Get());
        }
        public IEnumerable<PortfolioVm> GetPortfolios()
        {
            return mapper.Map<IEnumerable<PortfolioVm>>(portfolio.Get());
        }
        public IEnumerable<Category_PortoflioVm> GetCategoryPortfolios()
        {
            return mapper.Map<IEnumerable<Category_PortoflioVm>>(category_Portoflio.Get());
        }
        [HttpGet]
        public IActionResult Index()
        {
            MultipleModels models = new MultipleModels();
            models.ApplicationUserVm = userVm();
            models.TestimonialsVmAll = TestimonialsVms();
            models.HomeVmAll = GetHome();
            models.ExpCompaniesPhotoVm = GetPhotosExp();
            models.PortfolioVmAll = GetPortfolios();
            models.Category_PortoflioVmAll = GetCategoryPortfolios();
            return View(models);
        }

        [HttpPost]
        public IActionResult CreateTestimonial(MultipleModels model)
        {
            if (ModelState.IsValid)
            {
                var obj = TestimonialFunction(model);
                testimonials.Create(obj);
                toastNotification.AddSuccessToastMessage("Your testimonial created successfully");
                return RedirectToAction("Index", "Home", new { Area = "Home" });
            }
            else
            {
                model.ApplicationUserVm = userVm();
                model.TestimonialsVmAll = TestimonialsVms();
                model.HomeVmAll = GetHome();
                model.ExpCompaniesPhotoVm = GetPhotosExp();
                model.PortfolioVmAll = GetPortfolios();
                model.Category_PortoflioVmAll = GetCategoryPortfolios();
                toastNotification.AddErrorToastMessage("Error !!!");
                return View("Index", model);
            }
        }

        public FileResult DownloadFile()
        {
            var data = mapper.Map<HomeVm>(home.Get().FirstOrDefault());

            return File(data.Cv, "application/pdf", "Cv Abdalrhman Adel.pdf");
        }
        [HttpGet]
        public IActionResult DetailsPortfoilo(int id)
        {
            var data = mapper.Map<PortfolioVm>(portfolio.GetByID(id));
            return View(data);
        }

    }
}
