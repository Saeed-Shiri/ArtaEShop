
using Ordering.Core.Entities;
using Ordering.Core.Repositories.Commnad;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories.Command;
public class CommnadRepository<T> : ICommandRepository<T> where T : EntityBase
{
    protected readonly OrderContext _context;

    public CommnadRepository(OrderContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context
            .AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
