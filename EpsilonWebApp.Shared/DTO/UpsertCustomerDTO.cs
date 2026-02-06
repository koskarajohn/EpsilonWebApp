using System.ComponentModel.DataAnnotations;

namespace EpsilonWebApp.Shared.DTO;

public class UpsertCustomerDTO
{
    public Guid? Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? ContactName { get; set; }
    
    [Required]
    [StringLength(100)]
    public string? Address { get; set; }
    
    [StringLength(100)]
    public string? City { get; set; }
    
    [StringLength(100)]
    public string? Region { get; set; }
    
    [StringLength(10)]
    public string? PostalCode { get; set; }
    
    [StringLength(100)]
    public string? Country { get; set; }
    
    [Phone]
    [Required]
    public string? Phone { get; set; }


    public static UpsertCustomerDTO GetFrom(CustomerDTO customer)
    {
        if (customer == null) return new UpsertCustomerDTO();

        return new UpsertCustomerDTO()
        {
            Id = customer.Id,
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