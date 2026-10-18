namespace Domain.Abstraction;


public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; set; }
    string? CreatedByUserId { get; set; }
    DateTimeOffset? UpdatedAt { get; set; }
    string? UpdatedByUserId { get; set; }
}

