using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modules.Common.Infrastructure.Database;
using Modules.Common.Infrastructure.Policies;
using Modules.Stocks.Infrastructure.Database;
using Modules.Stocks.Infrastructure.Policies;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddStocksInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var sqlConnectionString = configuration.GetConnectionString("SqlServer");

		services.AddDbContext<StocksDbContext>(x => x
			.UseSqlServer(sqlConnectionString, sqlOptions =>
				sqlOptions.MigrationsHistoryTable(DbConsts.MigrationHistoryTableName, DbConsts.StocksSchemaName))
		);

		services.AddScoped<IModuleDatabaseMigrator, StocksDatabaseMigrator>();
		services.AddSingleton<IPolicyFactory, StocksPolicyFactory>();

		return services;
	}
}