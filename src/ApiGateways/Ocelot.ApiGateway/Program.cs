using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile(config =>
{
    config.Path = $"ocelot.{builder.Environment.EnvironmentName}.json";
    config.Optional = true;
    config.ReloadOnChange = true;
});

builder.Services
    .AddOcelot()
    .AddCacheManager(settings =>
    {
        settings.WithDictionaryHandle();
    }); 

var app = builder.Build();


// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

 app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello Ocelot Gateway");
});

await app.UseOcelot();



