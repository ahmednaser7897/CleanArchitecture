using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);

        builder.Property(x => x.Name)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255).IsRequired();

        // add new relationship with Course
        // courseId is FK ,so it is required
        builder.HasOne(x => x.Course)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.CourseId)
        //section must has a course (Required)
        .IsRequired();

        // add new relationship with Instructor
        // instructorId is FK ,so it is optional
        builder.HasOne(x => x.Instructor)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.InstructorId)
        //section may has an instructor (Optional)
        .IsRequired(false);

        //one to one relationship between section and schedule
        // one section can have only one schedule 
        // and one schedule can have many sections
        //so we can add ScheduleId to Section table
        builder.HasOne(x => x.Schedule)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.ScheduleId)
        //section must has a schedule (Required)
        .IsRequired();


        // many to many relationship with Students via Enrollment table
        builder.HasMany(x => x.Students)
       .WithMany(x => x.Sections)
       .UsingEntity<Enrollment>();

        builder.Property(x => x.StartTime)
            .IsRequired().HasColumnType("TIME");

        builder.Property(x => x.EndTime)
            .IsRequired().HasColumnType("TIME");

        builder.ToTable("Sections");
    }

}
