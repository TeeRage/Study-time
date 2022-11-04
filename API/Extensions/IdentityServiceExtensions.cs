using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

//This extension is used in Startup.cs settings for bringing these services. Keeps startup clean.
namespace API.Extensions
{
    //Static class, no need to create new instanse to use it
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            //Service for authentication using JWT authentication scheme
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true, //We want to have symmetric signing key with encoding 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false, //API server
                        ValidateAudience = false //Angular application
                    };
                }
            );

            return services;
        }
    }
}