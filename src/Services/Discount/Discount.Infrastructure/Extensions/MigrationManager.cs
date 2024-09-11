

using Discount.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Discount.Infrastructure.Extensions;
public static class MigrationManager
{
    private static readonly IDatabaseFactory databaseFactory;

    public static IDatabaseFactory DatabaseFactory => databaseFactory;

    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerService = services.GetRequiredService<ILogger<IDatabaseFactory>>();
        var configurationService = services.GetRequiredService<IConfiguration>();
        try
        {
            loggerService.LogInformation("Discount DB Migration Started");
            ApplyMigrations(DatabaseFactory);
            loggerService.LogInformation("Discount DB Migration Completed");
        }
        catch (Exception e)
        {
            loggerService.LogError(e, $"An error occured while migration db:{nameof(IDatabaseFactory)}");
            Console.WriteLine(e);
            //throw;
        }

        return host;
    }

    private static void ApplyMigrations(IDatabaseFactory databaseFactory)
    {
        databaseFactory.ApplyMigrations();
    }
}
