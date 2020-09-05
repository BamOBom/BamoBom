using Common.Utilities;
using Data.Repositories;
using Entities;
using EnumValue;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Services.DataInitializer
{
    public class UserDataInitializer : IDataInitializer
    {
        private readonly IUserRepository userRepository;
        private readonly RoleManager<Role> roleManager;

        public UserDataInitializer(IUserRepository userRepository ,
            RoleManager<Role> roleManage)
        {
            this.userRepository = userRepository;
            this.roleManager = roleManage;
        }

        public void InitializeData()
        {
            #region User
            if (!userRepository.TableNoTracking.Any(p => p.UserName == "Admin"))
            {
                userRepository.Add(new User
                {
                    UserName = "Admin",
                    Email = "admin@site.com",
                    NormalizedUserName = "ADMIN",
                    FirstName = "علیرضا",
                    Surname = " حمزه لوی",
                    PhoneNumber = " 0912 057 8716",
                    PhoneNumberConfirmed = true,
                    Gender = GenderType.Male,
                    IsActive = true,
                    UserType = UserType.Admin,
                    Status = Status.Approved,
                    EmailConfirmed = true,
                    ProfileImage = "User.png",
                    CityId = 1,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = "AL25whqFxOCbH4nPK5Mlpkf7dEZ+p53HwaPh9245hzktezx34U5+0sx6b+8pgGmbVQ==",
                    LastLoginDate = DateTime.Now,
                });
            }
            #endregion
        }
    }
}
