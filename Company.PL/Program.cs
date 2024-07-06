using AutoMapper;
using AutoMapper.Configuration;
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
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services handller
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanyContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
            )
            );//CLR will create object from company context one time and any ctr want it ref it only(Dependency Injection)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper((ele) => ele.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper((ele) => ele.AddProfile(new DepartmentMapperProfile()));
            builder.Services.AddIdentity<ApplicationModel, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireUppercase = true;

            }).AddEntityFrameworkStores<CompanyContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((Options) =>
            {
                Options.LoginPath = "Account/Login";
                Options.AccessDeniedPath = "Home/Error";
            });
            #endregion
            var app=builder.Build();
            var env = builder.Environment;
            #region Configure http pipeline
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
            #endregion
            app.Run();




        }


    }
}
