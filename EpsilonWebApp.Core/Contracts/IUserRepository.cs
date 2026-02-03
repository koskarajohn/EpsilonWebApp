using EpsilonWebApp.Core.Entities;

namespace EpsilonWebApp.Core.Contracts;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}