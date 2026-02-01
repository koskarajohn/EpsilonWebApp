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
    public string? Phone { get; set; }
}