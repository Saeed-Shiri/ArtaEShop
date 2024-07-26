namespace Catalog.Infrastructure.Data;

public class DatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
    public string BrandsCollection { get; set; }
    public string TypesCollection { get; set; }
}