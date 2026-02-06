using EpsilonWebApp.Shared.DTO;

namespace EpsilonWebApp.Core.Entities;

public class Customer : BaseEntity
{
    public string ContactName { get; set; }
    public string Address { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string Phone { get; set; }


    public static Customer Create(UpsertCustomerDTO dto)
    {
        var customer = new Customer()
        {
            Id = dto.Id!.Value,
            ContactName = dto.ContactName,
            Address = dto.Address,
            City = dto.City,
            Region = dto.Region,
            PostalCode = dto.PostalCode,
            Country = dto.Country,
            Phone = dto.Phone,
        };

        return customer;
    }
}