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

        public UserController(IUserRepository userRepository, IJwtService jwtService,
           UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager,
           IRepository<City> CityService)
        {
            this.userRepository = userRepository;
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._CityService = CityService;
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
                            var userCity = await userRepository.TableNoTracking.Where(e=>e.CityId == LocationId).ToListAsync(cancellationToken);
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
                            var userProvince = await userRepository.TableNoTracking.Include(e=>e.City).Where(e => e.City.ProvinceId == LocationId).ToListAsync(cancellationToken);
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
