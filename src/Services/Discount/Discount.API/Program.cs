using Discount.API.Services;
using Discount.Application;
using Discount.Grpc.Protos;
using Discount.Infrastructure;
using Discount.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();
app.MigrateDatabase();
// Configure the HTTP request pipeline.

app.MapGrpcService<DiscountService>();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(
        "Communication with gRPC endpoints must be made through a gRPC client.");
});

app.Run();

