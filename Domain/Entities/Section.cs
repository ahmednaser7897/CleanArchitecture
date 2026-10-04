
namespace Domain.Entities;

public class Section : IEntity<int>
{
    public int Id { get; set; }
    public required string Name { get; set; } = null!;

    // where section "must" has a course (Required)
    public required int CourseId { get; set; }
    public required Course Course { get; set; } = null!;

    // where section "may" has an instructor (Optional)
    public int? InstructorId { get; set; }
    public Instructor? Instructor { get; set; }

    public ICollection<Student> Students { get; set; } = [];

    // where section "must" has a Schedule (Required)
    public required int ScheduleId { get; set; }
    public required Schedule Schedule { get; set; } = null!;
    public required TimeSpan StartTime { get; set; }
    public required TimeSpan EndTime { get; set; }

    // public TimeSlot TimeSlot { get; set; } = null!;
    // public override string ToString()
    // {
    //     return $"Section Name: {SectionName} | Id: {Id} ";
    // }
}
// public class TimeSlot
// {
//     public TimeSpan StartTime { get; set; }
//     public TimeSpan EndTime { get; set; }

//     public override string ToString()
//     {
//         return $"{StartTime.ToString("hh\\:mm")} - {EndTime.ToString("hh\\:mm")}";
//     }
// }

