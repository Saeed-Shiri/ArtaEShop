using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);


var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

await builder.Services
    .MigrateDatabase<OrderContext>(async (context, services) =>
    {

        var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
        await OrderContextSeed.SeedAsync(context, logger);
    });

await app.RunAsync();
