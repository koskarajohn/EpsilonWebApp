using EpsilonWebApp.Core.Contracts;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.Core.Features.Customers.DeleteCustomer;

public class DeleteCustomer : IDeleteCustomer
{
    private ICustomerRepository _customerRepository;
    private ILogger<DeleteCustomer> _logger;

    public DeleteCustomer(ICustomerRepository customerRepository, ILogger<DeleteCustomer> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ErrorOr<Success>> InvokeAsync(Guid customerId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete customer with id: {customerId}", customerId);

        var affectedRows = await _customerRepository.DeleteAsync(customerId, cancellationToken).ConfigureAwait(false);
        if(affectedRows == 0)
            return Error.NotFound($"Customer does not exist.");

        return Result.Success;
    }
}