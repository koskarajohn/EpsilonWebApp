using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.SQLServer.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected ApplicationDbContext _dbContext;
    protected ILogger _logger;

    public BaseRepository(ApplicationDbContext context, ILogger logger)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public virtual async  Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        return await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        int rowsAffected = await _dbContext.Set<T>()
                                            .Where(x => x.Id == id)
                                            .ExecuteDeleteAsync(cancellationToken).ConfigureAwait(false);
        return rowsAffected;
    }
}