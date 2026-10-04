using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255).IsRequired();

        // add new relationship with offer
        // officeId is FK ,so it is optional
        builder.HasOne(x => x.Office)
        .WithOne(x => x.Instructor)
        .HasForeignKey<Instructor>(x => x.OfficeId)
        .IsRequired();



        builder.ToTable("Instructors");
    }
}
