using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Constants;
using MyBlog.Data;
using MyBlog.Data.Models;
using System;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            return app;
        }

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
            => app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Articles}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "custom",
                    pattern: "custom/{controller}/{action}");

                endpoints.MapRazorPages();
            });

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
            => SeedDataAsync(app).GetAwaiter().GetResult();

        private static async Task<IApplicationBuilder> SeedDataAsync(IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            var db = serviceScope.ServiceProvider.GetService<MyBlogDbContext>();

            await db.Database.MigrateAsync();

            await AddAdminRole(serviceScope.ServiceProvider);
            await AddAdminUser(serviceScope.ServiceProvider);

            await AddRegularRole(serviceScope.ServiceProvider);

            return app;
        }

        private static async Task AddAdminRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var existingRole = await roleManager.FindByNameAsync(WebConstants.AdminRoleName);

            if (existingRole != null)
            {
                return;
            }

            var adminRole = new IdentityRole(WebConstants.AdminRoleName);

            await roleManager.CreateAsync(adminRole);
        }

        private static async Task AddRegularRole(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var existingRole = await roleManager.FindByNameAsync(WebConstants.RegularRoleName);

            if (existingRole != null)
            {
                return;
            }

            var regularRole = new IdentityRole(WebConstants.RegularRoleName);

            await roleManager.CreateAsync(regularRole);
        }

        private static async Task AddAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();
            var existingUser = await userManager.FindByNameAsync(WebConstants.DefaultAdmin_Username);

            if (existingUser != null)
            {
                return;
            }

            var adminUser = new User
            {
                UserName = WebConstants.DefaultAdmin_Username,
                Email = WebConstants.DefaultAdmin_Email,
                SecurityStamp = WebConstants.DefaultAdmin_SecurityStamp
            };

            await userManager.CreateAsync(adminUser, WebConstants.DefaultAdmin_Password);

            await userManager.AddToRoleAsync(adminUser, WebConstants.AdminRoleName);
        }
    }
}
