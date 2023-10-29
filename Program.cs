using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoticeBoard.Core.Service;
using NoticeBoard.Core;
using NoticeBoard.Persistence;
using NoticeBoard.Persistence.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Mvc;
using NoticeBoard.Core.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace NoticeBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
            builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IPersonalDataService, PersonalDataService>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<FormOptions>(o =>
            {
                // o.ValueLengthLimit = 16777216;
            });

            var app = builder.Build();

            //app.UseStatusCodePages(async statusCodeContext => await Results.Problem(
            //    detail: "¯¹danie przes³ane do serwera zawiera b³êdy",
            //    statusCode: statusCodeContext.HttpContext.Response.StatusCode)
            //     .ExecuteAsync(statusCodeContext.HttpContext));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}