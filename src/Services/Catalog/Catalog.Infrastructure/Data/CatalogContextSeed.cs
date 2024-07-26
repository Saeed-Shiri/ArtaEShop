using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        var checkProducts = productCollection.Find(x => true).Any();
        string currentDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) ?? string.Empty;
        var path = Path.Combine(currentDir,"Data", "SeedData", "products.json");
        if (!checkProducts)
        {
            var productsData = File.ReadAllText(path);
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            foreach (var product in products)
            {
                productCollection.InsertOneAsync(product);
            }
        }
    }
}