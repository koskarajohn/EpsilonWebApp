using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Core.Entities;
using EpsilonWebApp.Shared.DTO;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.Core.Features.Customers.CreateCustomer;

public class CreateCustomer : ICreateCustomer
{
    private ICustomerRepository _customerRepository;
    private ILogger<CreateCustomer> _logger;

    public CreateCustomer(ICustomerRepository customerRepository, ILogger<CreateCustomer> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ErrorOr<UpsertCustomerDTO>> InvokeAsync(UpsertCustomerDTO customer, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Request - {@customer}", customer);
        
        customer.Id = Guid.NewGuid();
        
        var model = Customer.Create(customer);
        await _customerRepository.AddAsync(model, cancellationToken).ConfigureAwait(false);

        return customer;
    }
}