using System;
using Common;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebFramework.Configuration;
using WebFramework.CustomMapping;
using WebFramework.Middlewares;
using WebFramework.Swagger;

namespace Api
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.AddMinimalMvc();

            services.AddElmah(Configuration, _siteSetting);

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddCustomApiVersioning();

            services.AddSwagger();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                   opt.TokenLifespan = TimeSpan.FromMinutes(2));

            return services.BuildAutofacServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.IntializeDatabase();

            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseElmah();

            app.UseSwaggerAndUI();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseCors();

            app.UseEndpoints(config =>
            {
                config.MapControllers(); // Map attribute routing
                //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
                //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
            //Using 'UseMvc' to configure MVC is not supported while using Endpoint Routing.
            //To continue using 'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
            //app.UseMvc();
        }
    }
}
