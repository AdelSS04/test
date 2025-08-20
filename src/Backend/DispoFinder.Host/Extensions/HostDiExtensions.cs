using DispoFinder.Host.Seeding;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class HostDiExtensions
{
	public static IServiceCollection AddWebHostDependencies(this IServiceCollection services)
	{
		services.AddScoped<SeedService>();

		return services;
	}
}
