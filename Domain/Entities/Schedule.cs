using Domain.Enums;
namespace Domain.Entities;

public class Schedule : IEntity<int>
{
    public int Id { get; set; }
    public ScheduleEnum Title { get; set; }
    public bool SUN { get; set; }
    public bool MON { get; set; }
    public bool TUE { get; set; }
    public bool WED { get; set; }
    public bool THU { get; set; }
    public bool FRI { get; set; }
    public bool SAT { get; set; }

    // schedule and section has one to many relationship
    // where schedule may has many sections (Optional)
    // where section must has one schedule (Required)
    public ICollection<Section> Sections { get; set; } = [];

    public override string ToString()
    {
        return $"Title: {Title} | Id: {Id}";
    }
}
