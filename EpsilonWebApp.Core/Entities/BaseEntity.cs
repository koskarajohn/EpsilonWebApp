namespace EpsilonWebApp.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    
}