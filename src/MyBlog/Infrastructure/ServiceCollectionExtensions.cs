using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MyBlog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyMvc(this IServiceCollection services)
        {
            services.AddControllersWithViews(options => options
                .Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddRazorPages();

            return services;
        }
    }
}
