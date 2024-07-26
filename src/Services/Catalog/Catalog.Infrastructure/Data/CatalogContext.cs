using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> Brands { get; }
    public IMongoCollection<ProductType> Types { get; }

    public CatalogContext(IOptions<DatabaseSettings> configuration)
    {
        var client = new MongoClient(configuration.Value.ConnectionString);
        var database = client.GetDatabase(configuration.Value.DatabaseName);
        
        Products = database.GetCollection<Product>(configuration.Value.CollectionName);
        Brands = database.GetCollection<ProductBrand>(configuration.Value.BrandsCollection);
        Types = database.GetCollection<ProductType>(configuration.Value.TypesCollection);
               
        BrandContextSeed.SeedData(Brands);
        TypeContextSeed.SeedData(Types);
        CatalogContextSeed.SeedData(Products);
    }
}