using Basket.Application.GrpcServices;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Basket.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        services.AddScoped<DiscountGrpcService>();
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(cfg =>
        {
            cfg.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]);
        });
        return services;
    }
}