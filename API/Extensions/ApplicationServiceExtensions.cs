using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

//This extension is used in Startup.cs settings for bringing these services. Keeps startup clean.
namespace API.Extensions
{
    //Static class, no need to create new instanse to use it
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //Add service for JWT token, lasts only htpp scope lifetime.
            services.AddScoped<ITokenService, TokenService>();

            //Add service for database with SQLite, so that we can connect to the DB. 
            services.AddDbContext<DataContext>(options => 
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection")); //Address is in configuration files
            });

            return services;
        }
    }
}