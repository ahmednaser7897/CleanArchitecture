using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);

        // Composite Primary Key
        // The combination SectionId + StudentId must be unique
        builder.HasKey(x => new { x.SectionId, x.StudentId });

        //GET THE TABLE NAME
        builder.ToTable("Enrollments");
    }

}
