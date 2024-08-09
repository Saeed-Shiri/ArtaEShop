using Catalog.Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> Brands { get; }
    public IMongoCollection<ProductType> Types { get; }

    public CatalogContext(IOptionsMonitor<DatabaseSettings> options)
    {
        var client = new MongoClient(options.CurrentValue.ConnectionString);
        var database = client.GetDatabase(options.CurrentValue.DatabaseName);
        
        Products = database.GetCollection<Product>(options.CurrentValue.CollectionName);
        Brands = database.GetCollection<ProductBrand>(options.CurrentValue.BrandsCollection);
        Types = database.GetCollection<ProductType>(options.CurrentValue.TypesCollection);
               
        BrandContextSeed.SeedData(Brands);
        TypeContextSeed.SeedData(Types);
        CatalogContextSeed.SeedData(Products);
    }
}