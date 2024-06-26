using System.Reflection;
using Services;
using Services.Mapper;
using Microsoft.OpenApi.Models;
using Repositories;
using Services.Interfaces;
using Services.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        /// 

        public static void AddPackage(this IServiceCollection services)
		{
			IConfiguration config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();

			//Add other service in nuget package
			services.AddEndpointsApiExplorer();
			services.AddCors(options =>
			{
				options.AddPolicy("AllowReactApp",
					builder =>
					{
						//builder.AllowAnyOrigin() //using for develop local host of React
						//	   .WithOrigins("https://vietafood.shop") // Update with your React app URL
						//	   .WithOrigins("https://localhost:5173")
						//	   .AllowAnyHeader()
						//	   .AllowAnyMethod()
						//	   .AllowCredentials();
                        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
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

			//Jwt
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                    };
                });
            services.AddSwaggerGen(c =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put Bearer + your token in the box below",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
                });
				c.EnableAnnotations();
            });

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
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IProductService, ProductService>();
			services.AddScoped<ICouponService, CouponService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IOrderDetailService, OrderDetailService>();
			services.AddScoped<ICustomerInformationService, CustomerInformationService>();
			services.AddScoped<IBulkInvoiceService, BulkInvoiceService>();

			//Other
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}
	}

}
