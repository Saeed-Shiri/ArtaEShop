

namespace Discount.Application.Abstractions;
public interface IDatabaseFactory
{
    void CreateDatabase(string dbName);
    void ApplyMigrations();
}
