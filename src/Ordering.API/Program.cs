using Microsoft.EntityFrameworkCore;
using Ordering.API;
using Ordering.Application;
using Ordering.Application.Common.Interface;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
	.AddApplicationService()
	.AddInfrastructureService(builder.Configuration)
	.AddApiService();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
	IServiceProvider services = scope.ServiceProvider;
	ILogger logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

	try
	{
		ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
		if (context.Database.IsSqlServer()) { context.Database.Migrate(); }

		ISeed seed = services.GetRequiredService<ISeed>();
		seed.SeedDefaults().Wait();
	}
	catch (Exception ex)
	{
		logger.LogError("An error while setting up infrastructure - migration, sequences and seed {Exception}", ex.Message);
		throw;
	}
}

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
