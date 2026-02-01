using EpsilonWebApp.Core.Entities;

namespace EpsilonWebApp.Core.Contracts;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<int> AddAsync(T entity, CancellationToken cancellationToken);
    //Task<int> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken);
}