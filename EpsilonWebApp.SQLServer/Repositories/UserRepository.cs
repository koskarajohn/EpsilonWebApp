using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.SQLServer.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger) : base(context, logger)
    {
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.Where(x => x.Email == email)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}