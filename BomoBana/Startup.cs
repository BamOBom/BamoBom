using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebFramework.CustomMapping;
using WebFramework.Configuration;
using System;
using WebFramework.Middlewares;
using Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using WebFramework.Services;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http;
using ElmahCore.Mvc;
using WebMarkupMin.AspNetCore3;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using System.IO.Compression;
using WebFramework.Extensions;
using System.IO;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.IIS;
using WebFramework.Swagger;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace BomoBana
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });

            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.InitializeAutoMapper();

            services.AddDbContext(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.AddMinimalMvc();

            //services.AddJwtAuthentication(_siteSetting.JwtSettings);

            services.AddControllersWithViews();

            services.AddElmah(Configuration, _siteSetting);

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("fa-Ir");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("fa-IR") };
                options.RequestCultureProviders.Clear();
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "AuthenticationMerket";
                options.Cookie.HttpOnly = false;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Admin/Account/Login";
                //options.ReturnUrlParameter = "loginReturnUrl";
                options.LogoutPath = "/Admin/Account/Logout";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;

            });
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[]
                 { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                    opt.TokenLifespan = TimeSpan.FromMinutes(2));
            services.AddTransient<BreadcrumbService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient();
            services.AddDirectoryBrowser();

            services.AddWebMarkupMin(
                options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.AllowCompressionInDevelopmentEnvironment = true;
                })
                .AddHtmlMinification(
                    options =>
                    {
                        options.MinificationSettings.RemoveRedundantAttributes = true;
                        options.MinificationSettings.RemoveHttpProtocolFromAttributes = true;
                        options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true;
                    })
                .AddHttpCompression();
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<CustomCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "image/svg+xml" });
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddAuthentication(IISServerDefaults.AuthenticationScheme);
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "Angular/dist";
            });
            return services.BuildAutofacServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpContext();

            app.UseResponseCompression();

            app.UseAuthentication();

            app.UseWebMarkupMin();

            app.IntializeDatabase();

            app.UseRequestLocalization();

            app.UseCustomExceptionHandler();

            app.UseHsts(env);

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseElmah();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                     name: "Admin",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapRazorPages();

            });
            var optionss = new RewriteOptions()
          .AddRedirectToHttpsPermanent();

       //     app.UseRewriter(optionss);

       //     using (StreamReader apacheModRewriteStreamReader =
       //File.OpenText("ApacheModRewrite.txt"))
       //     using (StreamReader iisUrlRewriteStreamReader =
       //         File.OpenText("IISUrlRewrite.xml"))
       //     {
       //         var options = new RewriteOptions()
       //             .AddRedirect("redirect-rule/(.*)", "redirected/$1")
       //             .AddRewrite(@"^rewrite-rule/(\d+)/(\d+)", "rewritten?var1=$1&var2=$2",
       //                 skipRemainingRules: true)
       //             .AddApacheModRewrite(apacheModRewriteStreamReader)
       //             .AddIISUrlRewrite(iisUrlRewriteStreamReader)
       //             //.Add(MethodRules.RedirectXmlFileRequests)
       //             //.Add(MethodRules.RewriteTextFileRequests)
       //             .Add(new RedirectImageRequests(".png", "/png-images"))
       //             .Add(new RedirectImageRequests(".jpg", "/jpg-images"));

       //         app.UseRewriter(options);
       //     }

            app.UseDefaultFiles();
            // This will add "Libs" as another valid static content location

            //app.Run(context => context.Response.WriteAsync(
            //    $"Rewritten or Redirected Url: " +
            //    $"{context.Request.Path + context.Request.QueryString}"));
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "Angular";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

    }

}
