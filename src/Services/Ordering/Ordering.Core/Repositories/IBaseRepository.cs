

using System.Linq.Expressions;
using Ordering.Core.Entities;

namespace Ordering.Core.Repositories;
public interface IBaseRepository<T> where T : EntityBase
{
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetByIdAsync(int id);
}
