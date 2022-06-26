using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Area("Identity")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ITestimonials testimonials;
        private readonly PortfolioContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUser applicationUser;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;

        public ProfileController(ITestimonials testimonials ,PortfolioContext context, UserManager<ApplicationUser> userManager, IApplicationUser applicationUser, IMapper mapper , IToastNotification toastNotification)
        {
            this.testimonials = testimonials;
            this.context = context;
            this.userManager = userManager;
            this.applicationUser = applicationUser;
            this.mapper = mapper;
            this.toastNotification = toastNotification;
        }

        public ApplicationUserVm ApplicationUserVms()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = applicationUser.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(applicationUser.GetByID(idUser));
        }
        public IEnumerable<TestimonialsVm> TestimonialsVms()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            return mapper.Map<IEnumerable<TestimonialsVm>>(testimonials.GetByIDUser(idUser));
        }
        [HttpGet]
        public IActionResult MyProfile()
        {
            MultipleModels models = new MultipleModels();
            models.ApplicationUserVm = ApplicationUserVms();
            models.TestimonialsVmAll = TestimonialsVms();
            //var idUser = userManager.GetUserId(User); // get user Id
            //var model = applicationUser.GetByID(idUser);
            //var data = mapper.Map<ApplicationUserVm>(model);
            return View(models);
        }

        [HttpPost]
        public IActionResult EditOrCreateInformation(MultipleModels model)
        {
            if (ModelState.IsValid)
            {
                var idUser = userManager.GetUserId(User); // get user Id
                var olddata = context.Users.Where(a => a.Id == idUser).FirstOrDefault();

                    string oldfilename = olddata.PhotoUrl;
                    if (model.ApplicationUserVm.Photo == null)
                    {
                        model.ApplicationUserVm.PhotoUrl = oldfilename;
                    }
               if(oldfilename != null)
               {
                  if (model.ApplicationUserVm.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles/PhotoProfile", oldfilename)))
                  {
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles/PhotoProfile", oldfilename));
                        string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/PhotoProfile/");
                        // 2) Get File Name
                        string FileName = Guid.NewGuid() + Path.GetFileName(model.ApplicationUserVm.Photo.FileName);

                        // 3) Merge Physical Path + File Name
                        string FinalPath = Path.Combine(PhysicalPath, FileName);

                        // 4) Save The File As Streams "Data Over Time"
                        using (var stream = new FileStream(FinalPath, FileMode.Create))
                        {
                            model.ApplicationUserVm.Photo.CopyTo(stream);
                        }

                        model.ApplicationUserVm.PhotoUrl = FileName;
                  }
               }
               else
               {
                    string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/PhotoProfile/");
                    // 2) Get File Name
                    string FileName = Guid.NewGuid() + Path.GetFileName(model.ApplicationUserVm.Photo.FileName);

                    // 3) Merge Physical Path + File Name
                    string FinalPath = Path.Combine(PhysicalPath, FileName);

                    // 4) Save The File As Streams "Data Over Time"
                    using (var stream = new FileStream(FinalPath, FileMode.Create))
                    {
                        model.ApplicationUserVm.Photo.CopyTo(stream);
                    }

                    model.ApplicationUserVm.PhotoUrl = FileName;
                }
                //var obj = mapper.Map<ApplicationUser>(model);
                //applicationUser.Update(obj);
                olddata.PhotoUrl = model.ApplicationUserVm.PhotoUrl;
                olddata.Name = model.ApplicationUserVm.Name;
                olddata.CompanyName = model.ApplicationUserVm.CompanyName;
                olddata.JobTitle = model.ApplicationUserVm.JobTitle;
                context.Entry(olddata).State = EntityState.Modified;
                context.SaveChanges();
                toastNotification.AddSuccessToastMessage("Your Information Updated successfully");
                return RedirectToAction("MyProfile", "Profile", new { Area = "Identity" });
                
            }
            //if (ModelState.IsValid)
            //{
            //  if (model.Id == null)
            //  {
            //        var old2 = context.Users.Where(a => a.Id == user.Id).AsNoTracking().FirstOrDefault();
            //    string oldfilename = old2.PhotoUrl;
            //    if (model.Photo == null)
            //    {
            //        model.PhotoUrl = oldfilename;
            //    }

            //        if (model.Photo != null)
            //        {
            //            if (oldfilename != null)
            //            {
            //                if (model.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles/PhotoProfile/", oldfilename)))
            //                {
            //                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PhotoFiles/PhotoProfile", oldfilename));
            //                    string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/PhotoProfile/");
            //                    // 2) Get File Name
            //                    string FileName = Guid.NewGuid() + Path.GetFileName(model.Photo.FileName);

            //                    // 3) Merge Physical Path + File Name
            //                    string FinalPath = Path.Combine(PhysicalPath, FileName);

            //                    // 4) Save The File As Streams "Data Over Time"
            //                    using (var stream = new FileStream(FinalPath, FileMode.Create))
            //                    {
            //                        model.Photo.CopyTo(stream);
            //                    }

            //                    model.PhotoUrl = FileName;
            //                }
            //            }
            //            else
            //            {

            //                string PhysicalPath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot", "PhotoFiles/PhotoProfile/");
            //                // 2) Get File Name
            //                string FileName = Guid.NewGuid() + Path.GetFileName(model.Photo.FileName);

            //                // 3) Merge Physical Path + File Name
            //                string FinalPath = Path.Combine(PhysicalPath, FileName);

            //                // 4) Save The File As Streams "Data Over Time"
            //                using (var stream = new FileStream(FinalPath, FileMode.Create))
            //                {
            //                    model.Photo.CopyTo(stream);
            //                }

            //                model.PhotoUrl = FileName;
            //            }

            //            var obj = mapper.Map<ApplicationUser>(model);
            //            applicationUser.Update(obj);
            //            toastNotification.AddSuccessToastMessage("Your Information Updated successfully");
            //            return RedirectToAction("MyProfile", "Profile", new { Area = "Identity" });
            //        }
            //  }
            //}
            //toastNotification.AddErrorToastMessage("Error !!!");
            return View(model);
        }

        public IActionResult UpdateTestim(int id)
        {
            var data = mapper.Map<TestimonialsVm>(testimonials.GetByID(id));
            return View(data);
        }
        [HttpPost]
        public IActionResult UpdateTestim(TestimonialsVm model)
        {
            if (ModelState.IsValid)
            {
                var obj = mapper.Map<Testimonials>(model);
                testimonials.Update(obj);

                toastNotification.AddSuccessToastMessage("Testimonials item Updated successfully");
                return RedirectToAction("MyProfile", "Profile", new { Area = "Identity" });
            }
            toastNotification.AddErrorToastMessage("Error !!!!!!!!");
            return RedirectToAction("MyProfile", "Profile", new { Area = "Identity" });
        }
        public IActionResult DetailsTestim(int id)
        {
            var data = mapper.Map<TestimonialsVm>(testimonials.GetByID(id));
            return View(data);
        }

        [HttpGet]
        public IActionResult DeleteTestim(int id)
        {
            var data = mapper.Map<TestimonialsVm>(testimonials.GetByID(id));
            return View(data);
        }
        [HttpPost]
        public IActionResult DeleteTestim(TestimonialsVm model)
        {
            if (ModelState.IsValid)
            {
                var obj = mapper.Map<Testimonials>(model);
                testimonials.Delete(obj);

                toastNotification.AddSuccessToastMessage("Testimonials item Deleted successfully");
                return RedirectToAction("MyProfile", "Profile", new { Area = "Identity" });
            }
            toastNotification.AddErrorToastMessage("Error !!!!!!!!");
            return RedirectToAction("MyProfile", "Profile", new { Area = "Identity" });
        }
    }
}