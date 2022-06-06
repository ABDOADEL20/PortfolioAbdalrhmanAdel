using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        #region Login (Sign in)
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        #endregion

        #region Registration (Sign up)

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        #endregion

        #region Logoff (Sign Out)

        // Dont change name to SignOut
        //[HttpPost]
        //public async Task<IActionResult> LogOff()
        //{
        //    await signInManager.SignOutAsync();
        //    return RedirectToAction("Login", "Account", new { area = "Identity" });
        //}

        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> ForgetPassword(ForgetPasswordVm model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //Get user by email
        //            var user = await userManager.FindByEmailAsync(model.Email);

        //            if (user != null)
        //            {
        //                // Generate Token
        //                var token = await userManager.GeneratePasswordResetTokenAsync(user);

        //                // Generate Reset Link
        //                var passwordResetLink = Url.Action("ResetPassword", "Account", new { area = "Identity", Email = model.Email, Token = token }, Request.Scheme);

        //                SendMailHelper.MailSender(new MailVm() { To = model.Email, Title = "Reset Password-TM", Message = passwordResetLink });

        //                //logger.Log(LogLevel.Warning, passwordResetLink);

        //                return RedirectToAction("ConfirmForgetPassword", "Account", new { area = "Identity" });
        //            }

        //        }

        //        return View(model);
        //    }
        //    catch (Exception)
        //    {
        //        return View(model);
        //    }

        //}

        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = await userManager.FindByEmailAsync(model.Email);
        //            if (user != null)
        //            {
        //                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

        //                if (result.Succeeded)
        //                {
        //                    return RedirectToAction("ConfirmResetPassword", "Account", new { area = "Identity" });
        //                }

        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError("", error.Description);
        //                }

        //                return View(model);
        //            }

        //            return RedirectToAction("ConfirmResetPassword", "Account", new { area = "Identity" });

        //        }

        //        return View(model);
        //    }
        //    catch (Exception)
        //    {
        //        return View(model);
        //    }
        //}
        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        #endregion
    }
}
