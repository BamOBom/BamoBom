using Common.Exceptions;
using Data.Repositories;
using Common.Utilities;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApi.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebFramework.Api;
using Microsoft.AspNetCore.Identity;
using EnumValue;
using Kavenegar;
using Kavenegar.Exceptions;

namespace MyApi.Controllers.v1
{
    [ApiVersion("1")]
    public class UsersController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UsersController> logger;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger, IJwtService jwtService,
            UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }

        [HttpGet("GetUser/{id:int}")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user2 = await userManager.FindByIdAsync(id.ToString());
            var role = await roleManager.FindByNameAsync("Admin");

            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
                return NotFound();

            await userManager.UpdateSecurityStampAsync(user);
            return user;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> Token([FromForm]TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            if (!tokenRequest.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new Exception("OAuth flow is not password.");

            //var user = await userRepository.GetByUserAndPass(username, password, cancellationToken);
            var user = await userManager.FindByNameAsync(tokenRequest.username);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, tokenRequest.password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");


            //if (user == null)
            //    throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwt = await jwtService.GenerateAsync(user);
            return new JsonResult(jwt);
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<ConfirmPhoneNumberDto>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var HasUser = await userRepository.FindByPhoneNumber(userDto.PhoneNumber,cancellationToken);
                if (HasUser != null)
                {
                    var token = await userManager.GenerateChangePhoneNumberTokenAsync(HasUser, HasUser.PhoneNumber);
                    var callback = new ConfirmPhoneNumberDto
                    {
                        HasRegister = true,
                        Token = token,
                        UserId = HasUser.Id,
                        PhoneNumber = HasUser.PhoneNumber,

                    };
                    var message = "مارکت ! کد فعال سازی ورود به سامانه : " + token;
                    var sms = await SendSms(userDto.PhoneNumber, message);
                    if (!sms)
                    {
                        return BadRequest("خطا در ارسال پیامک");
                    }
                    else
                    {
                        return Ok(callback);
                    }
                }
                else
                {
                    var user = new User
                    {
                        PhoneNumber = userDto.PhoneNumber,
                        IsActive = false,
                        UserType = UserType.SimpleUser,
                        Status = Status.PendingApproval,
                        //IsDeleted = false,
                        PhoneNumberConfirmed = false,
                        UserName = userDto.PhoneNumber,
                        ProfileImage = "User.png",
                        CityId = 1,
                        TwoFactorEnabled = true,
                    };
                    var resultUser = await userManager.CreateAsync(user, userDto.PhoneNumber);
                    if (resultUser.Succeeded)
                    {
                        bool hasrole = await roleManager.RoleExistsAsync("UserAndroid");
                        if (!hasrole)
                        {
                            var Role = await roleManager.CreateAsync(new Role
                            {
                                Name = "UserAndroid",
                                Description = "کاربر اندروید"
                            });
                        }                     
                        var result3 = await userManager.AddToRoleAsync(user, "UserAndroid");
                        var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                        var callback = new ConfirmPhoneNumberDto
                        {
                            HasRegister = false,
                            Token = token,
                            UserId = user.Id,
                            PhoneNumber = user.PhoneNumber,

                        };
                        var message = "مارکت ! کد فعال سازی ثبت نام شما : " + token;
                        var sms = await SendSms(user.PhoneNumber, message);
                        if (!sms)
                        {
                            ModelState.AddModelError("", "خطا در ارسال پیامک");
                            return BadRequest();
                        }
                        else
                        {
                            return Ok(callback);
                        }
                    }
                    else
                    {
                        return NotFound(resultUser.Errors);
                    }
                }             
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("ConfirmTokenRegister")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<ConfirmTokenRegisterDto>> ConfirmTokenRegister(int UserId , string token, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var User = await userRepository.GetByIdAsync(cancellationToken, UserId);
                if (User == null)
                    return NotFound();
                var result = await userManager.VerifyChangePhoneNumberTokenAsync(User, token, User.PhoneNumber);
                if (result)
                {
                    ConfirmTokenRegisterDto model = new ConfirmTokenRegisterDto
                    {
                        Message = "تائید شد",
                        Success = true
                    };
                    return Ok(model);
                }
                else
                {
                    ConfirmTokenRegisterDto model = new ConfirmTokenRegisterDto
                    {
                        Message = "خطا !کد صحیح نمیباشد , کد منقضی شده است",
                        Success = false
                    };
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("[action]")]
        [AllowAnonymous]
        public virtual async Task<ActionResult> ComplateRegister(ContinueRegisterDto ConfirmModelDto, CancellationToken cancellationToken)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var User = await userRepository.GetByIdAsync(cancellationToken, ConfirmModelDto.UserId);
                if (User != null)
                {
                    User.FirstName = ConfirmModelDto.FirstName;
                    User.Surname = ConfirmModelDto.SurName;
                    User.PasswordHash = SecurityHelper.GetSha256Hash(ConfirmModelDto.Password);
                    User.CityId = ConfirmModelDto.CityId;
                    User.Gender = ConfirmModelDto.Gender;
                    User.LastLoginDate = DateTime.Now;
                    User.IsActive = true;
                    User.Status = Status.Approved;

                    await userRepository.UpdateAsync(User, cancellationToken);

                    var jwt = await jwtService.GenerateAsync(User);
                    return new JsonResult(jwt);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //[HttpPut]
        //public virtual async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        //{
        //    var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

        //    updateUser.UserName = user.UserName;
        //    updateUser.PasswordHash = user.PasswordHash;
        //    updateUser.FirstName = user.FirstName;
        //    updateUser.Surname = user.Surname;
        //    updateUser.CityId = user.CityId;
        //    updateUser.PhoneNumber = user.PhoneNumber;
        //    updateUser.Gender = user.Gender;
        //    updateUser.IsActive = user.IsActive;
        //    updateUser.LastLoginDate = user.LastLoginDate;
        //    updateUser.IsActive = true;
        //    updateUser.UserType = UserType.SimpleUser;
        //    updateUser.Status = Status.Approved;
        //    updateUser.IsDeleted = false;

        //    await userRepository.UpdateAsync(updateUser, cancellationToken);

        //    return Ok();
        //}

        //[HttpDelete]
        //public virtual async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        //{
        //    var user = await userRepository.GetByIdAsync(cancellationToken, id);
        //    await userRepository.DeleteAsync(user, cancellationToken);

        //    return Ok();
        //}

        #region Helper

        #region sms
        private async Task<bool> SendSms(string phoneNumber, string message)
        {
            try
            {
                var api = new KavenegarApi("30727A4E2F4175644B7967416D4E7378306D7362573379303362615A6E764773");
                var result = api.Send("10004000400505", phoneNumber, message);
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

        #endregion

    }
}
