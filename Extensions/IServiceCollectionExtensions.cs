using Microsoft.EntityFrameworkCore;
using TestTaskDotnet.Interfaces;
using TestTaskDotnet.Models.Base;
using TestTaskDotnet.Services;

namespace TestTaskDotnet.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                //options.UseLazyLoadingProxies();
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddScoped<Func<AppDbContext>>((provider) => () => provider.GetService<AppDbContext>());

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IRequestService, RequestService>()
                .AddScoped<IUserService, UserService>()
                ;
        }
    }
}
