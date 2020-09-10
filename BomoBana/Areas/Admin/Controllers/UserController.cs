using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using BomoBana.Areas.Admin;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using BomoBana.Controllers;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using EnumValue;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Common;
using System.IO;
using Common.Utilities;
using System;
using WebFramework.Services;

namespace BomoBana.Area.Admin
{
    [Area("Admin")]
    [AllowAnonymous]
    public class UserController : BaseController
    {
        private readonly IUserRepository userRepository;
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRepository<City> _CityService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserController(IUserRepository userRepository, IJwtService jwtService,
           UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager,
           IRepository<City> CityService, IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._CityService = CityService;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        #region Index

        public async Task<ActionResult<List<AdminPanelUserDtoViewModel>>> Index(int? LocationId,
            DisplayUserInLocations DisplayUserInLocations, CancellationToken cancellationToken)
        {
            try
            {
                if (LocationId == null && DisplayUserInLocations == DisplayUserInLocations.All)
                {
                    var user = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
                    if (user != null)
                    {
                        List<AdminPanelUserDtoViewModel> userlist = await GetUser(user);
                        if (userlist != null)
                        {
                            return View(userlist);
                        }
                    }
                }
                else
                {
                    switch (DisplayUserInLocations)
                    {
                        case DisplayUserInLocations.City:
                            var userCity = await userRepository.TableNoTracking.Where(e => e.CityId == LocationId).ToListAsync(cancellationToken);
                            if (userCity != null)
                            {
                                List<AdminPanelUserDtoViewModel> userlist = await GetUser(userCity);
                                if (userlist != null)
                                {
                                    return View(userlist);
                                }
                            }
                            break;
                        case DisplayUserInLocations.Province:
                            var userProvince = await userRepository.TableNoTracking.Include(e => e.City).Where(e => e.City.ProvinceId == LocationId).ToListAsync(cancellationToken);
                            if (userProvince != null)
                            {
                                List<AdminPanelUserDtoViewModel> userlist = await GetUser(userProvince);
                                if (userlist != null)
                                {
                                    return View(userlist);
                                }
                            }
                            break;
                        default:
                            var user = await userRepository.TableNoTracking.ToListAsync(cancellationToken);

                            if (user != null)
                            {
                                List<AdminPanelUserDtoViewModel> userlist = await GetUser(user);
                                if (userlist != null)
                                {
                                    return View(userlist);
                                }
                            }
                            break;
                    }
                }
                return View("Error");
            }
            catch (System.Exception ex)
            {
                return View("Error");
            }
        }

        #endregion

        #region Create

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AdminPanelCreateUserDto Model, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
            {
                AddNotification(NotifyType.Warning, OperationMessage.ModelStateIsNotValid.ToDisplay(), true);
                return View(Model);
            }
            try
            {
                var HasUser = await userRepository.FindByPhoneNumber(Model.PhoneNumber, cancellationToken);
                if (HasUser == null)
                {
                    var hasher = new PasswordHasher<User>();
                    var user = new User
                    {
                        PhoneNumber = Model.PhoneNumber,
                        IsActive = true,
                        UserType = Model.UserType,
                        Status = Model.Status,
                        PhoneNumberConfirmed = true,
                        UserName = Model.PhoneNumber,
                        ProfileImage = "User.png",
                        CityId =Convert.ToInt32(Model.CityId),
                        TwoFactorEnabled = false,
                        FirstName = Model.FirstName,
                        Surname = Model.SurName,
                        Gender = Model.Gender,
                        LastLoginDate = DateTime.Now,
                        NormalizedUserName = Model.PhoneNumber.ToUpperInvariant()
                    };
                        user.PasswordHash = hasher.HashPassword(user, Model.Password);
                    if (Model.ProfileImage.Length > 0)
                    {
                        string LName = Guid.NewGuid().ToString("D") + Model.ProfileImage.FileName;
                        using (var fileStream = new FileStream(Path.Combine(webHostEnvironment.WebRootPath,
                            ApplicationPathes.User.VirtualUploadFolder, LName), FileMode.Create))
                        {
                            await Model.ProfileImage.CopyToAsync(fileStream);
                            user.ProfileImage = LName;
                        }
                    }
                    var resultUser = await userManager.CreateAsync(user, SecurityHelper.GetSha256Hash(Model.Password));
                    if (resultUser.Succeeded)
                    {
                        bool hasrole = await roleManager.RoleExistsAsync(Model.UserType.ToDisplay());
                        if (!hasrole)
                        {
                            var Role = await roleManager.CreateAsync(new Role
                            {
                                Name = Model.UserType.ToDisplay(),
                                Description = Model.UserType.ToDisplay()
                            });
                        }
                        var result3 = await userManager.AddToRoleAsync(user,Model.UserType.ToDisplay());
                        var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                        var result = await userManager.VerifyChangePhoneNumberTokenAsync(user, token, user.PhoneNumber);
                        if (result)
                        {
                            var message = "بام و بوم !اطلاعات شما در سامانه ثبت شد ، از همکاری با شما خرسندیم  ";
                            var sms = await SmsService.SendSms(user.PhoneNumber, message);
                            if (!sms)
                            {
                                AddNotification(NotifyType.Warning, "خطا در ارسال پیامک", true);

                            }
                            else
                            {
                                AddNotification(NotifyType.Info, "پیامک ارسال شد", true);
                            }
                            AddNotification(NotifyType.Success, OperationMessage.OperationSucceed.ToDisplay(), true);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            AddNotification(NotifyType.Error, OperationMessage.OperationFailed.ToDisplay(), true);
                            return View(Model);
                        }
                    }
                    else
                    {
                        AddNotification(NotifyType.Error, OperationMessage.OperationFailed.ToDisplay(), true);
                        return View(Model);
                    }
                }
                else
                {
                    AddNotification(NotifyType.Warning, OperationMessage.DoestExist.ToDisplay(), true);
                    return View(Model);
                }
            }
            catch (Exception)
            {
                ErrorNotification(OperationMessage.OperationFailed.ToDisplay(), true);
                return RedirectToAction("Error", "Home");
            }

        }
        #endregion

        #region Helper
        private async Task<string> GetCity(int? id)
        {
            string lacation;
            if (id != null)
            {
                var city = await _CityService.TableNoTracking.Include(e => e.Province).ThenInclude(e => e.Country)
                    .FirstOrDefaultAsync(e => e.Id == id);
                lacation = city.Province.Country.Name + "/" + city.Province.Name + "/" + city.Name;
            }
            else
            {
                lacation = "وارد نشده است";
            }
            return lacation;
        }

        private async Task<List<AdminPanelUserDtoViewModel>> GetUser(List<User> users)
        {
            List<AdminPanelUserDtoViewModel> model = new List<AdminPanelUserDtoViewModel>();
            foreach (User item in users)
            {
                model.Add(new AdminPanelUserDtoViewModel
                {
                    FullName = item.FullName,
                    Id = item.Id,
                    IsActive = item.IsActive,
                    PhoneNumber = item.PhoneNumber,
                    ProfileImage = item.ProfileImage,
                    Status = item.Status,
                    UserName = item.UserName,
                    UserType = item.UserType,
                    Location = await GetCity(item.CityId),
                });
            }
            return model;
        }
        #endregion
    }
}
