using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Offices;

public class OfficeDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must be at most 100 characters long")]
    [MinLength(5, ErrorMessage = "Name must be at least 10 characters long")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Location is required")]
    public required string Location { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public string? InstructorName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWriting)]
    public int? InstructorId { get; set; }

    public static OfficeDto FromEntit(Office model)
    {
        return new OfficeDto
        {
            Id = model.Id,
            Name = model.Name,
            Location = model.Location,
            InstructorName = model.Instructor?.User?.Name,
            InstructorId = model.InstructorId,
        };
    }
    public Office ToEntit()
    {
        return new Office
        {
            Id = Id,
            Name = Name,
            Location = Location,
        };
    }
}

