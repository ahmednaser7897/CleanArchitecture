using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Enrollments;

public class EnrollmentDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }

    [Required(ErrorMessage = "SectionId is required")]
    public required int SectionId { get; set; }

    [Required(ErrorMessage = "StudentId is required")]
    public required int StudentId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public string? SectionName { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public string? StudentName { get; set; }

    public static EnrollmentDto FromEntit(Enrollment model)
    {
        return new EnrollmentDto
        {
            Id = model.Id,
            SectionId = model.SectionId,
            StudentId = model.StudentId,
            SectionName = model.Section?.Name,
            StudentName = model.Student?.User?.Name
        };
    }

    public Enrollment ToEntit()
    {
        return new Enrollment
        {
            Id = Id,
            SectionId = SectionId,
            StudentId = StudentId,
        };
    }
}
