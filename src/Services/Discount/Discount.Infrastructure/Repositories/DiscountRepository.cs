using Dapper;
using Discount.Application.Abstractions;
using Discount.Core.Entities;
using Discount.Core.Repositories;

namespace Discount.Infrastructure.Repositories;
public class DiscountRepository : IDiscountRepository
{
    private readonly IDatabaseConnectionFactory _connectionFactory;

    public DiscountRepository(IDatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connetion = _connectionFactory
            .CreateConnection();

        var affected = await connetion
            .ExecuteAsync(
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new
            {
                ProdutName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
            });
        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connetion = _connectionFactory
            .CreateConnection();

        var affected = await connetion
            .ExecuteAsync(
            "DELETE FROM Coupon WHERE ProdutName = @ProdutName",
            new
            {
                ProdutName = productName,
            });
        return affected != 0;
    }

    public async Task<Coupon?> GetDiscount(string productName)
    {
        await using var connetion = _connectionFactory
            .CreateConnection();

        var coupon = await connetion
            .QueryFirstOrDefaultAsync<Coupon>(
                "SELECT * FROM Coupopon WHERE ProductName = @ProductName",
                new
                { 
                    productName = productName
                });

        return coupon;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connetion = _connectionFactory
            .CreateConnection();

        var affected = await connetion
            .ExecuteAsync(
            "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount) WHERE Id = @Id",
            new
            {
                ProdutName = coupon.ProductName,
                Description = coupon.Description,
                Amount = coupon.Amount,
                Id = coupon.Id
            });
        return affected != 0;
    }
}
