using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using NToastNotify;
using PortfolioAbdo.BL.Interface;
using PortfolioAbdo.BL.Mapper;
using PortfolioAbdo.BL.Repository;
using PortfolioAbdo.DAL.DataBase;
using PortfolioAbdo.DAL.Extend;
using PortfolioAbdo.Languages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioAbdo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = true,
                PositionClass = ToastPositions.TopRight,
                PreventDuplicates = true,
                CloseButton = true
            });

            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
                })
                 .AddNewtonsoftJson(opt => {
                     opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 });

            services.AddDbContextPool<PortfolioContext>(opts =>
            opts.UseSqlServer(Configuration.GetConnectionString("PortfolioConnection")));

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "659381666869-n1ulav8h66ckfib7eqv3voeim8bs373u.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "570149051173955";
                    options.AppSecret = "4f37dfebd63ecd9cec51a5bc79f3e760";
                });

            services.AddScoped<IHome, HomeRepository>();
            services.AddScoped<ICategory_Portoflio, Category_PortoflioRepositroy>();
            services.AddScoped<IPortfolio, PortfolioRepository>();
            services.AddScoped<IExpCompaniesPhoto, ExpCompaniesPhotoRepository>();
            services.AddScoped<IApplicationUser, ApplicationUserRepository>();
            services.AddScoped<ITestimonials, TestimonialsRepository>();


            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
             options.LoginPath = "/Identity/Account/Login";
             options.AccessDeniedPath = "/Identity/Account/Login";
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<PortfolioContext>()
             .AddDefaultTokenProviders()
             .AddDefaultUI()
             .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            var supportedCultures = new[] {
                      new CultureInfo("ar-EG"),
                      new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
                }
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseNToastNotify();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                  name: "default",
                  areaName: "Home",
                  pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });

        }
    }
}
