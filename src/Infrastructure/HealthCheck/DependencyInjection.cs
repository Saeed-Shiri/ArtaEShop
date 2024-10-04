using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace HealthCheck;

public static class DependencyInjection
{
    public static IHealthChecksBuilder AddSqlServerHealthCheck(this IHealthChecksBuilder healthChecks, IConfiguration configuration)
    {
        healthChecks.AddSqlServer(configuration.GetSection("HealthCheksSetting")["OrderDb"], name: "Orderingdb - SQL Server", failureStatus: HealthStatus.Degraded, tags: ["Microsoft SQL Server", "Ordering Service"]);
        return healthChecks;
    }

    public static IHealthChecksBuilder AddMongoDbHealthCheck(this IHealthChecksBuilder healthChecks, IConfiguration configuration)
    {
        healthChecks.AddMongoDb(configuration.GetSection("HealthCheksSetting")["CatalogDb"], "CatalogDb - Mongo", HealthStatus.Degraded, tags: ["MongoDb", "Catalog Service"]);
        return healthChecks;
    }

    public static IHealthChecksBuilder AddPostgreSqlHealthCheck(this IHealthChecksBuilder healthChecks, IConfiguration configuration)
    {
        healthChecks.AddNpgSql(configuration.GetSection("HealthCheksSetting")["DiscountDb"], healthQuery: "select 1", name: "DiscountDb - NpgSql", failureStatus: HealthStatus.Degraded, tags: ["PostgreSQL", "Discsount Service"]);
        return healthChecks;
    }

    public static IHealthChecksBuilder AddRedisHealthCheck(this IHealthChecksBuilder healthChecks, IConfiguration configuration)
    {
        healthChecks.AddRedis(configuration.GetSection("HealthCheksSetting")["BasketDb"], "BasketDb - Redis", HealthStatus.Degraded, tags: ["Redis", "Basket Service"]);
        return healthChecks;
    }

    public static IHealthChecksBuilder AddRabbitMqHelthCheck(this IHealthChecksBuilder healthChecks, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        healthChecks.AddRabbitMQ(setup =>
        {
            var connection = serviceProvider.GetService<IConnection>();
            var connectionFactory = serviceProvider.GetService<IConnectionFactory>();
            if (connection != null)
            {
                setup.Connection = connection;
            }
            else if (connectionFactory != null)
            {
                setup.ConnectionFactory = connectionFactory;
            }
            else
            {
                throw new ArgumentException($"Either an IConnection or IConnectionFactory must be registered with the service provider");
            }
        }, "Message Broker - RabbitMQ", HealthStatus.Degraded, tags: ["RabbitMq", "Message Broker"]);

        //healthChecks.AddRabbitMQ(configuration["HealthCheksSetting:MessageBroker"], HealthStatus.Degraded, tags: ["RabbitMq", "Message Broker"]);
        return healthChecks;
    }
}
