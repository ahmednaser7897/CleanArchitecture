using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Sections;

public class SectionDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(255, ErrorMessage = "Name must be at most 255 characters long")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "CourseId is required")]
    public required int CourseId { get; set; }

    public int? InstructorId { get; set; }

    [Required(ErrorMessage = "ScheduleId is required")]
    public required int ScheduleId { get; set; }

    [Required(ErrorMessage = "StartTime is required")]
    public required TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "EndTime is required")]
    public required TimeSpan EndTime { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public string? CourseName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public string? InstructorName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public string? ScheduleTitle { get; set; }



    public static SectionDto FromEntit(Section model)
    {
        return new SectionDto
        {
            Id = model.Id,
            Name = model.Name,
            CourseId = model.CourseId,
            CourseName = model.Course?.Name,
            InstructorId = model.InstructorId,
            InstructorName = model.Instructor?.User?.Name,
            ScheduleId = model.ScheduleId,
            ScheduleTitle = model.Schedule?.Title.ToString(),
            StartTime = model.StartTime,
            EndTime = model.EndTime,
        };
    }

    public Section ToEntit()
    {
        return new Section
        {
            Id = Id,
            Name = Name,
            CourseId = CourseId,
            InstructorId = InstructorId,
            ScheduleId = ScheduleId,
            StartTime = StartTime,
            EndTime = EndTime,
            Course = null!,
            Schedule = null!
        };
    }
}
