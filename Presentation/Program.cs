using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddPackage();
builder.Services.AddMasterServices();

builder.Services.AddDbContext<VietaFoodDbContext>(options =>
{
	var conectionStr = builder.Configuration.GetConnectionString("DefaultConnectionStringDB");
	options.UseSqlServer(conectionStr, options => options.MigrationsAssembly("Presentation"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
