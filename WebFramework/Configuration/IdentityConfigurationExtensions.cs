using Microsoft.Extensions.DependencyInjection;
using Entities;
using Common;
using Microsoft.AspNetCore.Identity;
using Data;
using System;

namespace WebFramework.Configuration
{
    public static class IdentityConfigurationExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<User, Role>(identityOptions =>
            {
                //Password Settings
                identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; //#@!
                identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                //UserName Settings
                identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
                identityOptions.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                //Singin Settings
                identityOptions.SignIn.RequireConfirmedEmail = false;
                //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = settings.RequireConfirmedPhoneNumber;
                identityOptions.SignIn.RequireConfirmedAccount = settings.RequireConfirmedAccount;

                //Lockout Settings
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                identityOptions.Lockout.AllowedForNewUsers = true;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }
    }

}
