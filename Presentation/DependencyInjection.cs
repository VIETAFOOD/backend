using AutoMapper;
using BusinessObjects.Mapper;
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
			services.AddSwaggerGen();
			services.AddEndpointsApiExplorer();

			//Mappers
			var config = new MapperConfiguration(cf =>
			{
				cf.AddProfile(new AutoMapperProfile());
			});
			services.AddSingleton<IMapper>(config.CreateMapper());


		}

		/// <summary>
		/// Create dependencies for service (interface) & service (class) or repository (interface) & repository (class)
		/// </summary>
		/// <param name="services"></param>
		public static void AddMasterServices(this IServiceCollection services)
		{
			#region Repositories
			services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>),typeof(IGenericRepository<>));
            #endregion

            // Add dependency injection for class and interface
            //EX:
            //services.AddScoped<IAccountRepository, AccountRepository>();
            //services.AddScoped<IAccountService, AccountService>();
        }
    }

}
