using System.Reflection;
using Services;
using Services.Mapper;
using Microsoft.OpenApi.Models;
using Repositories;

namespace Presentation
{
	/// <summary>
	/// Functions for create dependency injections
	/// </summary>
	public static class DependencyInjection
	{
		/// <summary>
		/// This function to add dependency injection for NuGet Package
		/// </summary>
		/// <param name="services"></param>
		public static void AddPackage(this IServiceCollection services)
		{
			//Add other service in nuget package
			services.AddEndpointsApiExplorer();
			services.AddCors(options =>
			{
				options.AddPolicy("AllowReactApp",
					builder =>
					{
						builder.WithOrigins("https://vietafood.shop/") // Update with your React app URL
							   .AllowAnyHeader()
							   .AllowAnyMethod()
							   .AllowCredentials();
					});
			});
			//Mappers
			//Add other service in nuget package
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "VietaFood API",
					Description = "An ASP.NET Core Web API for VietaFood System",
				});

				// using System.Reflection;
				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
			});
			services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
		}

		/// <summary>
		/// Create dependencies for service (interface) & service (class) or repository (interface) & repository (class)
		/// </summary>
		/// <param name="services"></param>
		public static void AddMasterServices(this IServiceCollection services)
		{
			#region Repositories
			services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            #endregion

            // Add dependency injection for class and interface
            //EX:
            //services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<IAccountService, AccountService>();
        }
    }

}
