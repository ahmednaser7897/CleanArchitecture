using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);
        //student and ApplicationUser has one to one relationship
        // where student must has an ApplicationUser (Required)
        // where ApplicationUser may has an student (Optional)
        builder.HasOne(s => s.User)
        .WithOne(u => u.Student)
        .HasForeignKey<Student>(s => s.UserId)
        .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Students");
    }

}
