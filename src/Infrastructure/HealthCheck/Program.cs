using HealthCheck;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddSingleton<IConnectionFactory>((c) =>
{
    return new ConnectionFactory() { Uri = new Uri(configuration["HealthCheksSetting:MessageBroker"]) };
});

builder.Services.AddHealthChecks()
            .AddSqlServerHealthCheck(configuration)
            .AddMongoDbHealthCheck(configuration)
            .AddPostgreSqlHealthCheck(configuration)
            .AddRedisHealthCheck(configuration)
            .AddRabbitMqHelthCheck(builder.Services.BuildServiceProvider())
            .AddCatalogServiceHealthCheck(configuration);

builder.Services.AddHealthChecksUI(opt =>
{ 
    opt.AddHealthCheckEndpoint("Arta EShop Infrastructures", configuration.GetSection("HealthCheksSetting")["HealthCheckUrl"]); //map health check api    
})
    .AddInMemoryStorage();
//    .AddSqlServerStorage(builder.Configuration.GetConnectionString("HealthCheckConnectoinString"), null, options =>
//{
//    options.MigrationsAssembly(typeof(Program).Assembly.FullName);  // Set the assembly for migrations
//});


var app = builder.Build();

// Configure the HTTP request pipeline.


//app.MapHealthChecks("health", new HealthCheckOptions()
//{
//    Predicate = _ => true,
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
//});

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
    }
});
app.MapHealthChecks("/healthcheck", new ()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.MapHealthChecksUI(options =>
{
    options.UIPath = "/dashboard";  // Set the path for the UI
    //options.ApiPath = "/health-ui-api";  // API path for UI
    //options.AddCustomStylesheet("./HealthCheck/Custom.css");
    options.PageTitle = "Arta Eshop Health Cheks";
});

//app.MapGet("/", async (HealthCheckService healthCheckService) =>
//{
//    HealthReport healthReport = await healthCheckService.CheckHealthAsync();
//    return healthReport.Status.ToString();
//});
//app.MapGet("/health", async (HttpContext context, HealthCheckService healthCheckService) =>
//{
//    HealthReport healthReport = await healthCheckService.CheckHealthAsync();
//    await UIResponseWriter.WriteHealthCheckUIResponse(context, healthReport);
//});

app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("/dashboard");
});

/*this is for healthchek Ui sql server storage*/
//await app.ApplyMigration();

app.Run();
