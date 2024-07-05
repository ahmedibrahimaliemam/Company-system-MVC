using AutoMapper;
using Comoany.Bl.Interfaces;
using Comoany.Bl.Repositories;
using Company.DL.Contexts;
using Company.DL.Models;
using Company.PL.MapperProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL
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
            services.AddControllersWithViews();
            services.AddDbContext<CompanyContext>(Options =>Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
            )
            );//CLR will create object from company context one time and any ctr want it ref it only(Dependency Injection)
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddAutoMapper((ele) => ele.AddProfile(new EmployeeProfile()));
            services.AddAutoMapper((ele) => ele.AddProfile(new DepartmentMapperProfile()));
            services.AddIdentity<ApplicationModel,IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric=true;
                Options.Password.RequireDigit=true;
                Options.Password.RequireUppercase=true;

            }).AddEntityFrameworkStores<CompanyContext>().AddDefaultTokenProviders();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((Options) =>
            {
                Options.LoginPath = "Account/Login";
                Options.AccessDeniedPath = "Home/Error";
            });

		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseRouting();
			app.UseAuthentication();

			app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
