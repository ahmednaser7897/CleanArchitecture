
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class SectionConfiguration
    : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(x => x.Course)
            .WithMany(x => x.Sections)
            .HasForeignKey(x => x.CourseId)
            .IsRequired();

        builder.HasOne(x => x.Instructor)
            .WithMany(x => x.Sections)
            .HasForeignKey(x => x.InstructorId)
            .IsRequired(false);

        builder.HasOne(x => x.Schedule)
            .WithMany(x => x.Sections)
            .HasForeignKey(x => x.ScheduleId)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired()
            .HasColumnType("TIME");

        builder.Property(x => x.EndTime)
            .IsRequired()
            .HasColumnType("TIME");

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.ToTable("Sections");
    }
}
