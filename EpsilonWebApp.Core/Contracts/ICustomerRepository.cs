using EpsilonWebApp.Core.Entities;
using EpsilonWebApp.Shared.DTO;

namespace EpsilonWebApp.Core.Contracts;

public interface ICustomerRepository : IBaseRepository<Customer>
{
    Task<int> UpdateCustomerAsync(UpsertCustomerDTO customer, CancellationToken cancellationToken);
}