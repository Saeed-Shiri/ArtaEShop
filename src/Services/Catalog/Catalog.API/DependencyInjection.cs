using Catalog.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Catalog.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddApiVersioning();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(cfg =>
        {
            cfg.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Catalog.API",
                Version = "v1",
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                
                options.Authority = "https://localhost:9008";
                options.Audience = "Catalog";

                /*if any vpn is truned on, below config may be required*/
                //options.BackchannelHttpHandler = new HttpClientHandler
                //{
                //    UseProxy = false,
                //};

                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    ValidateIssuer = false,
                //    //ValidIssuer = "https://localhost:9009",
                //    ValidateAudience = false,
                //    //ValidateLifetime = true,
                //    //ValidateIssuerSigningKey = true
                //};

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"Authentication challenge: {context.Error}");
                        return Task.CompletedTask;
                    }
                };

            });
        return services;
    }
}
