

using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data;
public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
    {
        if (!context.Orders.Any())
        {
            await context.AddRangeAsync(await GetOrders());
            await context.SaveChangesAsync();
            logger.LogInformation($"Ordering Database seeded: {nameof(OrderContext)}");
        }
    }

    private static async Task<IEnumerable<Order>> GetOrders()
    {
        return await Task.FromResult(new List<Order>()
        {
            new Order()
            {
                UserName = "saeed",
                FirstName = "Saeed",
                LastName = "Shiri",
                EmailAddress = "saeed@artaeshop.com",
                AddressLine = "Tehran",
                Country = "Iran",
                TotalPrice = 850,
                State = "Tehran",
                ZipCode = "3356113354",

                CardName = "visa",
                CardNumber = "1234567890123456",
                Expiration = "12/25",
                Cvv = "123",
                PaymentMethod = 1,
            }
        });
    }
}
