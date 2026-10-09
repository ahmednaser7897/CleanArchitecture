using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Courses;

public class CourseDto
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenReading)]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name must be at most 100 characters long")]
    [MinLength(5, ErrorMessage = "Name must be at least 10 characters long")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(1000, 10000, ErrorMessage = "Price must be in range of 1000 to 10000.")]
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

