using System.Data.Common;
using Discount.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Persistence;
public class NpgsqlConnectionFactory : IDatabaseConnectionFactory
{
    private readonly IConfiguration _configuration;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbConnection CreateConnection()
    {
        return new NpgsqlConnection(
            _configuration.GetConnectionString("DefaultConnection"));

    }
}
