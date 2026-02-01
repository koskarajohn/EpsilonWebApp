using ErrorOr;

namespace EpsilonWebApp.Core.Features.Customers.DeleteCustomer;

public interface IDeleteCustomer
{
    Task<ErrorOr<Success>> InvokeAsync(Guid customerId, CancellationToken cancellationToken);
}