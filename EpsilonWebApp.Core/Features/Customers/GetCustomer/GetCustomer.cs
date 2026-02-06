using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Shared.DTO;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.Core.Features.Customers.GetCustomer;

public class GetCustomer : IGetCustomer
{
    private ICustomerRepository _customerRepository;
    private ILogger<GetCustomer> _logger;

    public GetCustomer(ICustomerRepository customerRepository, ILogger<GetCustomer> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ErrorOr<CustomerDTO>> InvokeAsync(Guid guid, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Customer with id - {guid}", guid);
        
        var customer = await _customerRepository.GetByIdAsync(guid, cancellationToken).ConfigureAwait(false);
        if (customer == null)
            return Error.NotFound($"Customer does not exist.");

        return new CustomerDTO()
        {
            Id = customer.Id,
            Address = customer.Address,
            City = customer.City,
            Country = customer.Country,
            ContactName = customer.ContactName,
            Phone = customer.Phone,
            PostalCode = customer.PostalCode,
            Region = customer.Region
        };
    }
}