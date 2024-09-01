

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositories.Commnad;
using Ordering.Core.Repositories.Query;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("OrderingConnectoinString"));
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

        return services;
    }
}
