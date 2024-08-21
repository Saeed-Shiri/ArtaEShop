using System.Data;
using System.Data.Common;


namespace Discount.Application.Abstractions;

public interface IDatabaseConnectionFactory
{
    DbConnection CreateConnection();
}
