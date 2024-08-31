

using Ordering.Core.Entities;

namespace Ordering.Core.Repositories;
public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IReadOnlyList<Order>> GetAllOrdersByUsername(string username);
}
