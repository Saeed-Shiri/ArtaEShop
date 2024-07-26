using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository, IBrandReposritory, ITypeRepository
{
    private readonly ICatalogContext _context;

    public ProductRepository(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context
            .Products
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context
            .Products
            .Find(x => true)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await _context
            .Products
            .Find(x => x.Name == name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Brands.Name, brand);
        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context
            .Products
            .InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context
            .Products
            .ReplaceOneAsync(x => x.Id == product.Id, product);
        return updateResult.IsAcknowledged && updateResult.MatchedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var deleteResult = await _context
            .Products
            .DeleteOneAsync(x => x.Id == id);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _context
            .Brands
            .Find(x => true)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _context
            .Types
            .Find(x => true)
            .ToListAsync();
    }
}