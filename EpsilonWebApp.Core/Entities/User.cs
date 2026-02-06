namespace EpsilonWebApp.Core.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsActive { get; set; } = true;
    
    public User(){}

    public User(string email, string password)
    {
        ArgumentNullException.ThrowIfNull(password, nameof(password));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        
        Email = email;
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }
}