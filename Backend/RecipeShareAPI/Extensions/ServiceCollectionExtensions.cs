using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecipeShare.API.Interfaces;
using RecipeShare.API.Managers;
using RecipeShare.Database;
using RecipeShareAPI.Models.Settings;

namespace RecipeShareAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRecipeShareServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add CORS policy for localhost only
            services.AddCors(options =>
            {
                options.AddPolicy("LocalhostOnly", policy =>
                {
                    policy.WithOrigins("http://localhost", "https://localhost")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
					policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
	                  .AllowAnyHeader()
	                  .AllowAnyMethod();
				});
            });

			services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            services.AddDbContext<SQLContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.MigrationsAssembly("RecipeShare.API");
			    });
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});

            services.AddScoped<IRecipeManager, RecipeManager>();

			return services;
        }
    }
}