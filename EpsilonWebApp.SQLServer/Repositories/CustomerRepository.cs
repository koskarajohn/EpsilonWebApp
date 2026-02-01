using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Core.Entities;
using EpsilonWebApp.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.SQLServer.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context, ILogger<BaseRepository<Customer>> logger) : base(context, logger)
    {
    }

    public async Task<int> UpdateCustomerAsync(UpsertCustomerDTO customer, CancellationToken cancellationToken)
    {
        var affectedRows = await _dbContext.Customers
            .Where(x => x.Id == customer.Id)
            .ExecuteUpdateAsync(setters
                => setters.SetProperty(x => x.ContactName, customer.ContactName)
                    .SetProperty(x => x.Address, customer.Address)
                    .SetProperty(x => x.City, customer.City)
                    .SetProperty(x => x.Country, customer.Country)
                    .SetProperty(x => x.PostalCode, customer.PostalCode)
                    .SetProperty(x => x.Region, customer.Region)
                    .SetProperty(x => x.Phone, customer.Phone));
        return affectedRows;
    }
}