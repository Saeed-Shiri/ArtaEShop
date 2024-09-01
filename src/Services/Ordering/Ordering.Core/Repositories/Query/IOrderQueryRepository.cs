
using Ordering.Core.Entities;

namespace Ordering.Core.Repositories.Query;
public interface IOrderQueryRepository : IQueryRespository<Order>
{
    Task<IReadOnlyList<Order>> GetAllOrdersByUsername(string username, CancellationToken cancellationToken = default);
}
