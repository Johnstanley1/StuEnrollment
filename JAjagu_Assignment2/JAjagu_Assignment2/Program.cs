using JAjagu_Assignment2.Entities;
using JAjagu_Assignment2.Services;
using Microsoft.EntityFrameworkCore;

namespace JAjagu_Assignment2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connString = builder.Configuration.GetConnectionString("CourseRegistrationDbJAjagu4315");
            builder.Services.AddDbContext<CourseRegistrationDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddScoped<ICourseRegistrationService, CourseRegistrationServices>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddMemoryCache();

            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}