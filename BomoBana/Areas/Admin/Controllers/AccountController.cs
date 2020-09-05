using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using BomoBana.Areas.Admin;
using Data.Repositories;
using System.Threading;
using EnumValue;
using System.Security.Claims;
using System;
using Kavenegar;
using Kavenegar.Exceptions;
using Common;

namespace BomoBana.Area.Admin
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IUserRepository userRepository, IJwtService jwtService,
           UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        #region Login

        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userRepository.FindByEmailOrPhoneAsync(model.Email, cancellationToken);
            if (user == null)
            {
                ModelState.AddModelError("", "این کاربری وجود ندارد");
                return View(model);
            }
            var isPasswordValid = await userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "گذرواژه صحیح نیست");
                return View(model);
            }
            //if (user.IsDeleted)
            //{
            //    ModelState.AddModelError("", "حساب کاربری شما حذف شده");
            //    return View(model);
            //}
            if (!user.IsActive)
            {
                ModelState.AddModelError("", "حساب کاربری شما غیر فعال شده");
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(user.UserName,
             model.Password, model.RemmeberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim("FullName", user.FullName));
                //identity.AddClaim(new Claim("ProfileImage", user.ProfileImage));
                identity.AddClaim(new Claim("CityId", user.CityId.ToString()));
                user.LastLoginDate = DateTime.Now;
                await userRepository.UpdateAsync(user, cancellationToken);
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    if (user.UserType == UserType.Admin)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    else if (user.UserType == UserType.Agency)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Agency");
                    }
                }
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new
                {
                    ReturnUrl = model.ReturnUrl,
                    model.RemmeberMe
                });
            }
            if (result.IsLockedOut)
            {
                var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
                var content = string.Format("حساب شما مسدود شده است ، برای تنظیم مجدد رمز ورود خود ، لطفاً روی این لینک کلیک کنید: {0}", forgotPassLink);

                //var message = new Message(new string[] { model.Email }, „Locked out account information“, content, null);
                //await _emailSender.SendEmailAsync(message);

                ModelState.AddModelError("", "حساب شما مسدود شده است");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError("", "تلاش برای ورود به سیستم ناموفق است");
                return View(model);
            }

        }
        #endregion

        #region LogOut
        public async Task<IActionResult> Logout()
        {
          
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        #region ForgotPassword
        //public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
        //{
        //    public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        //    {
        //        var username = await manager.GetUserNameAsync(user);
        //        if (username.ToLower().Equals(password.ToLower()))
        //            return IdentityResult.Failed(new IdentityError { Description = "Username and Password can't be the same.", Code = "SameUserPass" });

        //        if (password.ToLower().Contains(„password“))
        //            return IdentityResult.Failed(new IdentityError { Description = "The word password is not allowed for the Password.", Code = "PasswordContainsPassword" });

        //        return IdentityResult.Success;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model.PhoneNumber);

            var user = await userRepository.FindByPhoneNumber(model.PhoneNumber, cancellationToken);
            if (user == null)
            {
                ModelState.AddModelError("", "کاربری با این شماره یافت نشد");
                return View(model);
            }
            var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            var callback = Url.Action(nameof(ResetPassword), nameof(AccountController), new { token, PhoneNumber = user.PhoneNumber }, Request.Scheme);

            var message = "بوم و بنا ! کد احراز هویت برای فراموشی رمز شما : " + token;
            var sms = await SendSms(user.PhoneNumber, message);
            if (!sms)
            {
                ModelState.AddModelError("", "خطا در ارسال پیامک");
                return View(model);
            }
            return View(nameof(VerifyPhoneNumber), new VerifyPhoneNumberViewModel { PhoneNumber = user.PhoneNumber,Code = "" });
        }

        [HttpGet]
        public IActionResult VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userfind = await userRepository.FindByPhoneNumber(model.PhoneNumber, cancellationToken);
            if (userfind == null)
            {
                ModelState.AddModelError("", "خطا ! کاربر یافت نشد");
                return View(model);
            }
            var result = await userManager.VerifyChangePhoneNumberTokenAsync(userfind, model.Code, model.PhoneNumber);
            if (result)
            {
                return View(nameof(ResetPassword),new ResetPasswordModel { Token = model.Code, PhoneNumber = model.PhoneNumber });
            }
            ModelState.AddModelError("", "کد صحیح نمیباشد");
            return View(model);
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword(ResetPasswordModel model)
        {
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await userRepository.FindByPhoneNumber(model.PhoneNumber, cancellationToken);
            if (user == null)
                if (user == null)
                {
                    ModelState.AddModelError("", "خطا ! کاربر یافت نشد");
                    return View(model);
                }

            var resetPassResult = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError("", "خطا ! کد احراز منقضی شده است");
                }
                return View(nameof(Login));
            }
            return RedirectToAction(nameof(Login) , new { ReturnUrl = "/" });
        }
        #endregion

        #region sms
        private async Task<bool> SendSms(string phoneNumber, string message)
        {
            try
            {
                var api = new KavenegarApi("6733627055695371726A4B384337657057474333726B34304E6765706F444A784D726A4F634E52762B43633D");
                var result = api.Send("1000596446", phoneNumber, message);
                return true;
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
                return false;

            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
                return false;

            }
        }

        #endregion
    }
}
