
using System.Linq.Expressions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Commnad;
using Ordering.Core.Repositories.Query;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories.Command;
public class OrderCommandRepository : CommnadRepository<Order>, IOrderCommandRepository
{
    public OrderCommandRepository(OrderContext context) : base(context)
    {
        
    }
}
