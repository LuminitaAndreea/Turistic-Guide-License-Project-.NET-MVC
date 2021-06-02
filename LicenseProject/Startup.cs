using BookloversApplication.Repositories;
using LicenseProject.Models;
using LicenseProject.Repositories;
using LicenseProject.Repositories.Interfaces;
using LicenseProject.Services;
using LicenseProject.Wrapper;
using LicenseProject.Wrapper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddIdentity<ApplicationUser, IdentityRole<int>>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();

            var connection = @"Server=DESKTOP-PNVFDPI\MSSQLSERVER02;Database=Licenta;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Models.Context>
                (options => options.UseSqlServer(connection));

            services.AddRazorPages();

            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IProjectWrapper, ProjectWrapper>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ITuristicObjectRepository, TuristicObjectRepository>();
            services.AddScoped<ITuristicObjectService, TuristicObjectService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRestaurantCategoryRepository, RestaurantCategoryRepository>();
            services.AddScoped<IRestaurantCategoryService, RestaurantCategoryService>();
            services.AddScoped<ITuristicObjectCategoryRepository, TuristicObjectCategoryRepository>();
            services.AddScoped<ITuristicObjectCategoryService, TuristicObjectCategoryService>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IRestaurantRecommenderService, RestaurantRecommenderService>();
            services.AddScoped<ITuristicObjectRecommenderService, TuristicObjectRecommenderService>();

            services.AddSession();
           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
