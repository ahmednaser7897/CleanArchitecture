using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Schedules;

public class ScheduleDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public ScheduleEnum Title { get; set; }

    public bool SUN { get; set; }
    public bool MON { get; set; }
    public bool TUE { get; set; }
    public bool WED { get; set; }
    public bool THU { get; set; }
    public bool FRI { get; set; }
    public bool SAT { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int SectionsCount { get; set; }

    public static ScheduleDto FromEntit(Schedule model)
    {
        return new ScheduleDto
        {
            Id = model.Id,
            Title = model.Title,
            SUN = model.SUN,
            MON = model.MON,
            TUE = model.TUE,
            WED = model.WED,
            THU = model.THU,
            FRI = model.FRI,
            SAT = model.SAT,
            SectionsCount = model.Sections?.Count ?? 0
        };
    }

    public Schedule ToEntit()
    {
        return new Schedule
        {
            Id = Id,
            Title = Title,
            SUN = SUN,
            MON = MON,
            TUE = TUE,
            WED = WED,
            THU = THU,
            FRI = FRI,
            SAT = SAT,
        };
    }
}
