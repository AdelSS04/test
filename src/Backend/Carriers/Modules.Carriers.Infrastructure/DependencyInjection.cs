using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modules.Carriers.Infrastructure.Database;
using Modules.Carriers.Infrastructure.Policies;
using Modules.Common.Infrastructure.Database;
using Modules.Common.Infrastructure.Policies;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddCarriersInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var sqlConnectionString = configuration.GetConnectionString("SqlServer");

		services.AddDbContext<CarriersDbContext>(x => x
			.UseSqlServer(sqlConnectionString, sqlOptions =>
				sqlOptions.MigrationsHistoryTable(DbConsts.MigrationHistoryTableName, DbConsts.CarriersSchemaName))
		);

		services.AddScoped<IModuleDatabaseMigrator, CarriersDatabaseMigrator>();
		services.AddSingleton<IPolicyFactory, CarriersPolicyFactory>();

		return services;
	}
}