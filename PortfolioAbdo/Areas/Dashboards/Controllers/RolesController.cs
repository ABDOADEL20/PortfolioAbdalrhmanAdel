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
    public class RolesController : Controller
    {
        private readonly IToastNotification toastNotification;
        private readonly IApplicationUser user;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(IToastNotification toastNotification, IApplicationUser user,IMapper mapper,RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.toastNotification = toastNotification;
            this.user = user;
            this.mapper = mapper;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public ApplicationUserVm userVm()
        {
            var idUser = userManager.GetUserId(User); // get user Id
            var model = user.GetByID(idUser);
            return mapper.Map<ApplicationUserVm>(model);
        }
        public IActionResult Index()
        {
            var Roles = roleManager.Roles;
            ViewData["Message"] = userVm();
            return View(Roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var role = new IdentityRole()
                    {
                        Name = model.Name,
                        NormalizedName = model.Name.ToUpper()
                    };
                    var result = await roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("Role Created successfully");
                        return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                toastNotification.AddErrorToastMessage("Error !!!!");
                return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage("Error !!!!");
                return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var role = await roleManager.FindByIdAsync(model.Id);

                    role.Name = model.Name;
                    role.NormalizedName = model.Name.ToUpper();

                    var result = await roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("Role Updated successfully");
                        return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                toastNotification.AddErrorToastMessage("Error !!!!");
                return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage("Error !!!!");
                return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return View(role);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = await roleManager.FindByIdAsync(id);

                    var result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("Role Deleted successfully");
                        return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
                    }
                    else
                    {
                        toastNotification.AddErrorToastMessage("Error !!!!");
                        return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
                    }
                }
                toastNotification.AddErrorToastMessage("Error !!!!");
                return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
            catch (Exception)
            {
                toastNotification.AddErrorToastMessage("Error !!!!");
                return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            ViewBag.RoleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleVm>();

            foreach (var user in userManager.Users)
            {
                var userInRole = new UserInRoleVm()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVm> model, string RoleId)
        {
            if (ModelState.IsValid) 
            { 
            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {

                    result = await userManager.AddToRoleAsync(user, role.Name);

                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (i < model.Count)
                    continue;
            }
            
            toastNotification.AddSuccessToastMessage("User Added to Role successfully");
            return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
            }
            toastNotification.AddErrorToastMessage("Error !!!!");
            return RedirectToAction("Index", "Roles", new { area = "Dashboards" });
        }

    }
}
