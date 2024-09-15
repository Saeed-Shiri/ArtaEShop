using HealthChecks.UI.Data;
using k8s.KubeConfigModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck;

public static class MigrationManager
{
    public static async Task ApplyMigration(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<HealthChecksDb>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<HealthChecksDb>>();

		try
		{
			
			var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
			if(pendingMigrations.Any())
			{
                logger.LogInformation($"Started Db Migration: {nameof(HealthChecksDb)} at {DateTime.Now}");

                await context.Database.MigrateAsync();

                logger.LogInformation($"Migration Completed: {nameof(HealthChecksDb)} at {DateTime.Now}");

            }
		}
		catch (Exception e)
		{
            logger.LogError(e, $"An error occurred while migrating db: {nameof(HealthChecksDb)}");
        }
    }
}
