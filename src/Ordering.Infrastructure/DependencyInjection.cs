
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Interface;
using Ordering.Application.Data;
using System.Reflection;

namespace Ordering.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
			services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

			// Configure DbContext to use SQL Server with UseSqlServer
			services.AddDbContext<ApplicationDbContext>((sp, options) =>
			{

				options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
				sqlServerOptionsAction: sqlOptions =>
				{
					// Set up migrations assembly
					sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
					sqlOptions.MigrationsHistoryTable("__OrderDbMigrationsHistory", "ordermanagement");
				});
			});


			// Register other services
			services.AddScoped<ISeed, Seed>();
			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
			return services;
		}
	}
}
