using Domain.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Owned]
public class RefreshToken : IEntity<int>
{
    public required string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public DateTime? RevokedAt { get; set; }
    public bool IsActive => RevokedAt == null && !IsExpired;
}

