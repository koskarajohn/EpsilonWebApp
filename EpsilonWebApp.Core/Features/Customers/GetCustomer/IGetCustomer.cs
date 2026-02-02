using EpsilonWebApp.Shared.DTO;
using ErrorOr;

namespace EpsilonWebApp.Core.Features.Customers.GetCustomer;

public interface IGetCustomer
{
    Task<ErrorOr<CustomerDTO>> InvokeAsync(Guid guid, CancellationToken cancellationToken);
}