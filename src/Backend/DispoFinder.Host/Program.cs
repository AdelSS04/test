using DispoFinder.Host.Seeding;
using Modules.Common.API.Extensions;
using Modules.Common.Infrastructure.Database;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebHostDependencies();
builder.AddCoreHostLogging();
builder.Services.AddCoreWebApiInfrastructure();
builder.Services.AddCoreInfrastructure(builder.Configuration,
[
	ShipmentsModuleRegistration.ActivityModuleName,
	CarriersModuleRegistration.ActivityModuleName,
	StocksModuleRegistration.ActivityModuleName
]);


builder.Services
	.AddShipmentsModule(builder.Configuration)
	.AddCarriersModule(builder.Configuration)
	.AddStocksModule(builder.Configuration);


builder.Services.AddScoped<SeedService>();


var app = builder.Build();


await using var scope = app.Services.CreateAsyncScope();
await scope.MigrateModuleDatabasesAsync();
var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
await seedService.SeedDataAsync();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();
app.UseModuleMiddlewares();
app.MapApiEndpoints();
await app.RunAsync();
