using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.
    AddApiServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

await builder.Services.MigrateDatabase<OrderContext>(async (context, services) =>
{
    
    var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
    await OrderContextSeed.SeedAsync(context, logger);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/api/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseHealthChecksUI(delegate (Options options)
{
    options.UIPath = "/healthcheck-ui";
    options.AddCustomStylesheet("./HealthCheck/Custom.css");

});
await app.RunAsync();
