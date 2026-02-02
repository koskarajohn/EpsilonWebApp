using EpsilonWebApp.Shared.DTO;
using ErrorOr;

namespace EpsilonWebApp.Core.Features.Customers.CreateCustomer;

public interface ICreateCustomer
{
    Task<ErrorOr<UpsertCustomerDTO>> InvokeAsync(UpsertCustomerDTO customer, CancellationToken cancellationToken);
}