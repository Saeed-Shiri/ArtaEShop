

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Discount.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddGrpc();
        return services;
    }
}
