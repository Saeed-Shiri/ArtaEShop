
using Dapper;
using Discount.Application.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Persistence;
public class NpgsqlDatabaseFactory : IDatabaseFactory
{
    private readonly IDatabaseConnectionFactory _connectionFactory;

    public NpgsqlDatabaseFactory(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void ApplyMigrations()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();
        using var cmd = new NpgsqlCommand()
        {
            Connection = connection as NpgsqlConnection
        };
        cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
        cmd.ExecuteNonQuery();
        cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                ProductName VARCHAR(500) NOT NULL,
                                                Description TEXT,
                                                Amount INT)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700);";
        cmd.ExecuteNonQuery();
    }

}
