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

        // add new relationship with offer
        // officeId is FK ,so it is optional
        builder.HasOne(x => x.Office)
        .WithOne(x => x.Instructor)
        .HasForeignKey<Instructor>(x => x.OfficeId)
        //Instructor must have an office (Required)
        .IsRequired();

        //Instructor and ApplicationUser has one to one relationship
        //where instructor must has an ApplicationUser (Required)
        //where ApplicationUser may has an instructor (Optional)
        builder.HasOne(i => i.User)
        .WithOne(u => u.Instructor)
        .HasForeignKey<Instructor>(i => i.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

        // Exclude soft-deleted courses from all queries by default.
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.ToTable("Instructors");
    }
}
