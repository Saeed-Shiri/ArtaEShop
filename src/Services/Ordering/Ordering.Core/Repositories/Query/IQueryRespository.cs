

using System.Linq.Expressions;
using Ordering.Core.Entities;

namespace Ordering.Core.Repositories.Query;
public interface IQueryRespository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
