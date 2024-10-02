

using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories.Commnad;
using Ordering.Core.Repositories.Query;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.EventBusConsumer;

namespace Ordering.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine($"Connection string => {configuration.GetConnectionString("OrderingConnectoinString")}");
        services.AddDbContext<OrderContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("OrderingConnectoinString"))
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine);
    });

        services.Scan(x => x.FromAssemblyOf<OrderContext>()
        .AddClasses(classes => classes.Where(type => type is { IsClass: true, IsPublic: true, IsAbstract: false })
        .AssignableTo(typeof(ICommandRepository<>)))
        .AsImplementedInterfaces(interfaceType => interfaceType != typeof(ICommandRepository<>))
        .WithScopedLifetime());

        services.Scan(x => x.FromAssemblyOf<OrderContext>()
        .AddClasses(classes => classes.Where(type => type is { IsClass: true, IsPublic: true, IsAbstract: false })
        .AssignableTo(typeof(IQueryRespository<>)))
        .AsImplementedInterfaces(interfaceType => interfaceType != typeof(IQueryRespository<>))
        .WithScopedLifetime());

        services.AddScoped<BasketOrderingConsumer>();

        services.AddMassTransit(config =>
        {
            config.AddConsumer<BasketOrderingConsumer>();
            config.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["EventBusSettings:HostAddress"]);
                //provide the queue name with consumer settings
                config.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, config =>
                {
                    config.ConfigureConsumer<BasketOrderingConsumer>(context);
                });
            });
        });

        return services;
    }
}
