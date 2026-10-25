
using Domain.Abstraction;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser, IAuditableEntity, ISoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string PhoneKey { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public DateTime? BirthDate { get; set; } = null;
    public Gender? Gender { get; set; }
    // Relationships
    public Student? Student { get; set; }
    public Instructor? Instructor { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    // Audit properties
    public DateTimeOffset? CreatedAt { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedByUserId { get; set; }
    // Soft-delete properties
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public string? DeletedByUserId { get; set; }
}
