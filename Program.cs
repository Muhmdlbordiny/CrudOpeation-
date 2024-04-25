using Assighment.Models;
using Assighment.Repositry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Assighment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            


            // Add services to the container.
            //built in need to register
            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc().AddSessionStateTempDataProvider();
            builder.Services.AddSession(options => {
            options.IdleTimeout = TimeSpan.FromMinutes(30);});
           string connectionString = builder.Configuration.GetConnectionString("Cs1");
            builder.Services.AddDbContext<AppDbcontext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString);
            });
            //register usermanager ,rolemanager==>userstore,rolestore
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>
                (option=>option.Password.RequireDigit=true)
                .AddEntityFrameworkStores<AppDbcontext>();
            // Custom Service--Register
            builder.Services.AddScoped<IStudentRepositry, StudentRepositry>();
            builder.Services.AddScoped<IDepartmentRepositry, Departmentrepositry>();
            builder.Services.AddScoped<IinstructorRepo, InstructorRepo>();
            builder.Services.AddScoped<ICourseRepo, CourseRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();



            app.UseRouting();
            app.UseAuthentication(); //request


            app.UseAuthorization();
           /* app.MapControllerRoute(
                "Route1","std/{id:int}",
            new
            {
                controller = "Student",
                action = "Details"
            }
                ) ;*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); // placeholder

            app.Run();
        }
    }
}
