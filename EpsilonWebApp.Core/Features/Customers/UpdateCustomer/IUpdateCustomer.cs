using EpsilonWebApp.Shared.DTO;
using ErrorOr;

namespace EpsilonWebApp.Core.Features.Customers.UpdateCustomer;

public interface IUpdateCustomer
{
    Task<ErrorOr<Success>> InvokeAsync(UpsertCustomerDTO customer, CancellationToken cancellationToken);
}