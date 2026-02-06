using EpsilonWebApp.Core.Contracts;
using EpsilonWebApp.Shared.DTO;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace EpsilonWebApp.Core.Features.Customers.GetCustomers;

public class GetCustomers : IGetCustomers
{
    
    private ICustomerRepository _customerRepository;
    private ILogger<GetCustomers> _logger;

    public GetCustomers(ICustomerRepository customerRepository, ILogger<GetCustomers> logger)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ErrorOr<IEnumerable<CustomerDTO>>> InvokeAsync(CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
        _logger.LogInformation("Retrieved {Count} customers", customers.Count());
        
        return customers.Select(x => new CustomerDTO()
        {
            Id = x.Id,
            Address = x.Address,
            City = x.City,
            Country = x.Country,
            ContactName = x.ContactName,
            Phone = x.Phone,
            PostalCode = x.PostalCode,
            Region = x.Region
        }).ToList();
    }
}