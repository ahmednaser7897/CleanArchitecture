namespace Domain.Abstraction;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
    string? DeletedByUserId { get; set; }
}

