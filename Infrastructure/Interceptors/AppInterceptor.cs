
using System.Security.Claims;
using Domain.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class AppInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        ApplyChanges(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyChanges(eventData.Context);
        return base.SavingChangesAsync(
            eventData, result, cancellationToken);
    }

    private void ApplyChanges(DbContext? context)
    {
        if (context is null)
            return;

        var now = DateTimeOffset.UtcNow;
        var userId = _httpContextAccessor
        .HttpContext?
        .User?
        .FindFirstValue(ClaimTypes.NameIdentifier);

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                ApplyCreatedAudit(entry, now, userId);
            }
            else if (entry.State == EntityState.Modified)
            {
                ApplyUpdatedAudit(entry, now, userId);
            }
            else if (entry.State == EntityState.Deleted)
            {
                ApplySoftDelete(entry, now, userId);
            }
        }
    }

    private static void ApplyCreatedAudit(
        EntityEntry entry,
        DateTimeOffset now,
        string? userId)
    {
        if (entry.Entity is not IAuditableEntity entity)
            return;

        if (entity.CreatedAt == default)
            entity.CreatedAt = now;

        entity.CreatedByUserId ??= userId;
    }

    private static void ApplyUpdatedAudit(
        EntityEntry entry,
        DateTimeOffset now,
        string? userId)
    {
        if (entry.Entity is not IAuditableEntity entity)
            return;

        entity.UpdatedAt = now;
        entity.UpdatedByUserId = userId;

        entry.Property(nameof(IAuditableEntity.CreatedAt))
            .IsModified = false;

        entry.Property(nameof(IAuditableEntity.CreatedByUserId))
            .IsModified = false;
    }

    private static void ApplySoftDelete(
        EntityEntry entry,
        DateTimeOffset now,
        string? userId)
    {
        if (entry.Entity is not ISoftDeleteEntity entity)
            return;

        entry.State = EntityState.Modified;

        entity.IsDeleted = true;
        entity.DeletedAt = now;
        entity.DeletedByUserId = userId;

        if (entry.Entity is IAuditableEntity audit)
        {
            audit.UpdatedAt = now;
            audit.UpdatedByUserId = userId;
        }
    }
}
