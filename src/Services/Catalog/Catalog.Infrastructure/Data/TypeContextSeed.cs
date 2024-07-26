using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class TypeContextSeed
{
    public static void SeedData(IMongoCollection<ProductType> typeCollection)
    {
        var checkTypes = typeCollection.Find(x => true).Any();
        string currentDir = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) ?? string.Empty;
        var path = Path.Combine(currentDir,"Data", "SeedData", "types.json");
        if (!checkTypes)
        {
            var typesData = File.ReadAllText(path);
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

            foreach (var type in types)
            {
                typeCollection.InsertOneAsync(type);
            }
        }
    }
}