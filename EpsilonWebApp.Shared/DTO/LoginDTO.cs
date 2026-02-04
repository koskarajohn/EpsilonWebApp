using System.ComponentModel.DataAnnotations;

namespace EpsilonWebApp.Shared.DTO;

public class LoginDTO
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}