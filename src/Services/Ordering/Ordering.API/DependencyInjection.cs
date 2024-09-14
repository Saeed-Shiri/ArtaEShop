
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Ordering.Infrastructure.Data;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddApiVersioning();
        services.AddHealthChecks()
            //.AddSqlServer(configuration.GetConnectionString("OrderingConnectoinString"), healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy, tags: new[] { "Feedback", "Database" })
            .Services.AddDbContext<OrderContext>();
        services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

        })
            .AddInMemoryStorage();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Ordering.API",
                Version = "v1",
            });
        });
        return services;
    }
}
