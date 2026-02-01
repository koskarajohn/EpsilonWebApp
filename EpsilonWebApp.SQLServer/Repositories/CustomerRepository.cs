using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Core.Entities;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.SQLServer.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, ILogger<BaseRepository<Customer>> logger) : base(context, logger)
    {
    }
}