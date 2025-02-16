using Catalog.Infrastructure;
using Catalog.Application;
using Catalog.API;


class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services
            .AddApiServices(builder.Configuration)
            .AddApplicationServices(builder.Configuration)
            .AddInfrastructureServices(builder.Configuration);


        var app = builder.Build();

        //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

        //var handler = new HttpClientHandler
        //{
        //    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
        //    UseProxy = false,
        //    Proxy = null,
        //    UseCookies = false
        //};
        //var httpClient = new HttpClient(handler)
        //{
        //    DefaultRequestHeaders = { Connection = { "close" } }
        //};

        //try
        //{
        //    var response = await httpClient.GetAsync("https://localhost:9009/.well-known/openid-configuration/jwks");
        //    var keys = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine($"✅ دریافت موفق JWKS: {keys}");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"❌ خطا در دریافت JWKS: {ex.Message}");
        //}
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();

    }
}



