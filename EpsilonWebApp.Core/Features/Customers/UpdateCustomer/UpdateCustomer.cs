using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Shared.DTO;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.Core.Features.Customers.UpdateCustomer;

public class UpdateCustomer : IUpdateCustomer
{
    
    private ICustomerRepository _customerRepository;
    private ILogger<UpdateCustomer> _logger;

    public UpdateCustomer(ICustomerRepository customerRepository, ILogger<UpdateCustomer> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public async Task<ErrorOr<Success>> InvokeAsync(UpsertCustomerDTO customer, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating customer {Id}", customer.Id);

        var affectedRows = await _customerRepository.UpdateCustomerAsync(customer, cancellationToken)
                                                      .ConfigureAwait(false);
        
        if(affectedRows == 0)
            return Error.NotFound($"Customer does not exist.");

        return Result.Success;
    }
}