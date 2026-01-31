namespace EpsilonWebApp.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
}