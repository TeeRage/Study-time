using System.Text;
using API.Extensions;
using API.Middleware;
using Microsoft.OpenApi.Models;
namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(_config); //Use extensions for JWT and SQLite services
            services.AddControllers();
            services.AddCors();
            services.AddIdentityServices(_config);//USe extension for authentication with JWT services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>(); //Use own middleware for exception handling
            
            app.UseHttpsRedirection();

            app.UseRouting();

            //Allow api usage with any headers and methods if used from localhost 4200
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            //Middleware for using authentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
