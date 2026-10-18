using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Abstraction;

public abstract class IEntity<TKey> : ISoftDeleteEntity, IAuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TKey Id { get; set; } = default!;

    public DateTimeOffset? CreatedAt { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedByUserId { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedByUserId { get; set; }
}
