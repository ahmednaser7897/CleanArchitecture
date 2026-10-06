using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.DTOs.Courses;

public class CourseDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int SectionsCount { get; set; }
    public static CourseDto FromEntit(Course model)
    {
        return new CourseDto
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            SectionsCount = model.Sections.Count
        };
    }
    public Course ToEntit()
    {
        return new Course
        {
            Id = Id,
            Name = Name,
            Price = Price,
        };
    }
}

