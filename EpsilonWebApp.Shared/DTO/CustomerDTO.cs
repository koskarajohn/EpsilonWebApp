namespace EpsilonWebApp.Shared.DTO;

public class CustomerDTO
{
    public Guid Id { get; set; }
    public string? CompanyName { get; set; }
    public string? ContactName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? Phone { get; set; }
    
    public static CustomerDTO GetFrom(UpsertCustomerDTO customer)
    {
        if (customer == null) return new CustomerDTO();

        return new CustomerDTO()
        {
            Id = customer.Id.HasValue ? customer.Id.Value : Guid.NewGuid(),
            ContactName = customer.ContactName,
            Country = customer.Country,
            Address = customer.Address,
            City = customer.City,
            PostalCode = customer.PostalCode,
            Region = customer.Region,
            Phone = customer.Phone
        };
    }
}