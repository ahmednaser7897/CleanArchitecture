using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(0, 1);

        // NOW its an ENUM SO NO NEED to set its type
        builder.Property(x => x.Title)
        .HasConversion(
            x => x.ToString(),
            x => (ScheduleEnum)Enum.Parse(typeof(ScheduleEnum), x)
        )
        .IsRequired();

        // IN SQL : BIT NOT NULL
        builder.Property(x => x.MON).IsRequired();
        builder.Property(x => x.TUE).IsRequired();
        builder.Property(x => x.WED).IsRequired();
        builder.Property(x => x.THU).IsRequired();
        builder.Property(x => x.FRI).IsRequired();
        builder.Property(x => x.SAT).IsRequired();
        builder.Property(x => x.SUN).IsRequired();


        builder.HasData(LoadSchedules());

        builder.ToTable("Schedules");
    }

    private static List<Schedule> LoadSchedules()
    {
        return
                  [
                      new Schedule { Id = 1, Title = ScheduleEnum.Daily, SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = false, SAT = false },
                    new Schedule { Id = 2, Title = ScheduleEnum.DayAfterDay, SUN = true, MON = false, TUE = true, WED = false, THU = true, FRI = false, SAT = false },
                    new Schedule { Id = 3, Title = ScheduleEnum.TwiceAWeek, SUN = false, MON = true, TUE = false, WED = true, THU = false, FRI = false, SAT = false },
                    new Schedule { Id = 4, Title = ScheduleEnum.Weekend, SUN = false, MON = false, TUE = false, WED = false, THU = false, FRI = true, SAT = true },
                    new Schedule { Id = 5, Title = ScheduleEnum.Compact, SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = true, SAT = true }
                  ];
    }

}
