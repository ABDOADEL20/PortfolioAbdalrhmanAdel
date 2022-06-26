using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NToastNotify;
using PortfolioAbdo.BL.Helper;
using PortfolioAbdo.BL.Models;
using PortfolioAbdo.DAL.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PortfolioAbdo.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IToastNotification toastNotification;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(ILogger<AccountController> logger, IToastNotification toastNotification, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.toastNotification = toastNotification;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        #region Login (Sign in)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginVm model = new LoginVm
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            try
            {
                model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null && !user.EmailConfirmed &&
                                (await userManager.CheckPasswordAsync(user, model.Password)))
                    {
                        ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                        return View(model);
                    }
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RemeberMe, false);

                    if (result.Succeeded)
                    {
                        toastNotification.AddSuccessToastMessage("Login successfully!");
                        return RedirectToAction("Index", "Home", new { area = "Home" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid UserName or Password");
                    }

                }
                toastNotification.AddErrorToastMessage("Please... check your email or password");
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        #endregion

        #region Registration (Sign up)

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        IsAgree = model.IsAgree
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                            new { area = "Identity", userId = user.Id, token = token }, Request.Scheme);
                        logger.Log(LogLevel.Warning, confirmationLink);
                        SendMailHelper.MailSender(new MailVm() { To = model.Email, Title = "Abdalrhman Adel Portfolio | Confirmation Email", Message = "Please click on this link to verify your account: " + confirmationLink });

                        toastNotification.AddWarningToastMessage("Registration successful,Before you can Login, please confirm your " +
                        "email, by clicking on the confirmation link we have emailed you");

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                toastNotification.AddErrorToastMessage("Please... check your information");
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        #endregion

        #region Logoff (Sign Out)

        //Dont change name to SignOut
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            toastNotification.AddSuccessToastMessage("Logout successfully!");
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Get user by email
                    var user = await userManager.FindByEmailAsync(model.Email);

                    if (user != null)
                    {
                        // Generate Token
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);

                        // Generate Reset Link
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { area = "Identity", Email = model.Email, Token = token }, Request.Scheme);

                        SendMailHelper.MailSender(new MailVm() { To = model.Email, Title = "Abdalrhman Adel Portfolio | Reset Password", Message = "Please click on this link to reset your password: " + passwordResetLink });

                        //logger.Log(LogLevel.Warning, passwordResetLink);

                        return RedirectToAction("ConfirmForgetPassword", "Account", new { area = "Identity" });
                    }
                    ModelState.AddModelError("", "This user is not registered");
                    return View(model);
                }

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }

        }

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
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("ConfirmResetPassword", "Account", new { area = "Identity" });
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }

                    return RedirectToAction("ConfirmResetPassword", "Account", new { area = "Identity" });

                }

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        #endregion

        #region External Login

        [AllowAnonymous]
        [HttpPost]
        public  IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                new { area = "Identity", ReturnUrl = returnUrl });
            var properties = signInManager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
         public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null) 
         {
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginVm model = new LoginVm
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", model);
            }
            // Get the login information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", model);
            }
            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            
            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            ApplicationUser user = null;

            if (email != null)
            {
                // Find the user
                user = await userManager.FindByEmailAsync(email);

                // If email is not confirmed, display login view with validation error
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View("Login", model);
                }
            }
            if (signInResult.Succeeded)
            {
                toastNotification.AddSuccessToastMessage("Login successfully!");
                return RedirectToAction("Index", "Home", new { area = "Home" });
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await userManager.CreateAsync(user);

                        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action("ConfirmEmail", "Account",
                            new { area = "Identity", userId = user.Id, token = token }, Request.Scheme);

                        logger.Log(LogLevel.Warning, confirmationLink);

                        SendMailHelper.MailSender(new MailVm() { To = user.Email, Title = "Abdalrhman Adel Portfolio | Confirmation Email", Message = "Please click on this link to verify your account: " + confirmationLink });

                        toastNotification.AddWarningToastMessage("Registration successful,Before you can Login, please confirm your " +
                        "email, by clicking on the confirmation link we have emailed you");

                        return RedirectToAction("Login");
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    toastNotification.AddSuccessToastMessage("Login successfully!");
                    return RedirectToAction("Index", "Home", new { area = "Home" });
                }
                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on abdoadel5@gmail.com";
                return View("Error");
            }
        }
        #endregion

        #region Confirm Email

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home", new { area = "Home" });
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                //toastNotification.AddErrorToastMessage($"The User ID {userId} is invalid");
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                toastNotification.AddSuccessToastMessage("Your account has been successfully confirmed!");
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        #endregion

    }
}
