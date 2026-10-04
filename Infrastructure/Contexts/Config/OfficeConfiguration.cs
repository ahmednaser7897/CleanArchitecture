using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);

        builder.Property(x => x.Name)
           .HasColumnType("VARCHAR")
           .HasMaxLength(255).IsRequired();


        builder.ToTable("Offices");
    }
}
