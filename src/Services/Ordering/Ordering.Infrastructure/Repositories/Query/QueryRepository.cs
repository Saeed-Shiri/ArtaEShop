

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Query;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories.Query;
public class QueryRepository<T> : IQueryRespository<T> where T : EntityBase
{
    protected readonly OrderContext _context;

    public QueryRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<T>()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<T>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<T>()
            .FindAsync(id, cancellationToken);
    }
}
