using EpsilonWebApp.Shared.DTO;
using ErrorOr;

namespace EpsilonWebApp.Core.Features.Customers.GetCustomers;

public interface IGetCustomers
{
    Task<ErrorOr<IEnumerable<CustomerDTO>>> InvokeAsync(CancellationToken cancellationToken);
}