using Ordering.Core.Entities;

namespace Ordering.Core.Repositories.Commnad;
public interface ICommandRepository<T> where T : EntityBase
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
