

using System.Net.WebSockets;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Extensions;
public static class MigrationManager
{
    public static async Task MigrateDatabase<TContext>(this IServiceCollection services, Func<TContext, IServiceProvider, Task> seeder) where TContext : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        await using var scope = serviceProvider.CreateAsyncScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbContext>>();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation($"Started Db Migration: {typeof(TContext).Name} at {DateTime.Now}");

            await CallSeeder(seeder, context, scope.ServiceProvider);

            logger.LogInformation($"Migration Completed: {typeof(TContext).Name} at {DateTime.Now}");
        }
        catch (SqlException e)
        {

            logger.LogError(e, $"An error occurred while migrating db: {typeof(TContext).Name}");
        }
    }

    private static async Task CallSeeder<TContext>(Func<TContext, IServiceProvider, Task> seeder, TContext context, IServiceProvider services) where TContext : DbContext
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }

        await seeder(context, services);
    }
}
