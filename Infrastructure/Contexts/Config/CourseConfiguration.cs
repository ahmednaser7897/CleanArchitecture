using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255).IsRequired();

        builder.Property(x => x.Price).IsRequired().HasPrecision(15, 2);

        builder.ToTable("Courses");
    }


}
