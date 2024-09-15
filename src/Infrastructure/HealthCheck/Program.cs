using HealthCheck;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine(builder.Configuration.GetConnectionString("HealthCheckConnectoinString"));
builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetSection("HealthCheksSetting")["OrderDb"], healthQuery: "select 1", name: "Orderingdb - SQL Server", failureStatus: HealthStatus.Degraded, tags: new[] { "Feedback", "Database" })
            .AddMongoDb(builder.Configuration.GetSection("HealthCheksSetting")["CatalogDb"], "CatalogDb - Mongo", HealthStatus.Degraded)
            .AddRedis(builder.Configuration.GetSection("HealthCheksSetting")["BasketDb"], "BasketDb - Redis", HealthStatus.Degraded)
            .AddNpgSql(builder.Configuration.GetSection("HealthCheksSetting")["DiscountDb"], healthQuery: "select 1", name: "DiscountDb - NpgSql", failureStatus: HealthStatus.Degraded, tags: new[] { "Feedback", "Database" });

builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
    opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
    opt.SetApiMaxActiveRequests(1); //api requests concurrency    
    opt.AddHealthCheckEndpoint("Arta EShop Services", "/health"); //map health check api    

}).AddSqlServerStorage(builder.Configuration.GetConnectionString("HealthCheckConnectoinString"), null, options =>
{
    options.MigrationsAssembly(typeof(Program).Assembly.FullName);  // Set the assembly for migrations
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});
app.UseHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";  // Set the path for the UI
    options.ApiPath = "/health-ui-api";  // API path for UI
    //options.AddCustomStylesheet("./HealthCheck/Custom.css");
    options.PageTitle = "Arta Eshop Health Cheks";
});


await app.ApplyMigration();

app.Run();
