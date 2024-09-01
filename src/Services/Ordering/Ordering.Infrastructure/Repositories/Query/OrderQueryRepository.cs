

using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Query;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories.Query;
public class OrderQueryRepository : QueryRepository<Order>, IOrderQueryRepository
{
    public OrderQueryRepository(OrderContext context) : base(context)
    {
        
    }
    public async Task<IReadOnlyList<Order>> GetAllOrdersByUsername(string username, CancellationToken cancellationToken = default)
    {
        return await _context
            .Orders
            .Where(x => string.Equals(x.UserName, username))
            .ToListAsync(cancellationToken);
    }
}
