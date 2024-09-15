
using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder webApplicationBuilder)
    {
        var services = webApplicationBuilder.Services;
        var configuration = webApplicationBuilder.Configuration;

        services.AddInfrastructureServices(configuration)
            .AddApiServices(configuration)
            .AddApplicationServices();

        return webApplicationBuilder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseExceptionHandler();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
