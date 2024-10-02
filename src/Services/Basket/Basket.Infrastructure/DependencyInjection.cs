
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("CacheSettings")["ConnectionString"];
        });

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["EventBusSettings:HostAddress"]);
            });
        });


        services.AddScoped<IBasketRepository, BasketRepository>();

        return services;
    }
}
