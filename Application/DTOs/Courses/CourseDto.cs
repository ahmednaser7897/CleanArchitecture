using Domain.Entities;

namespace Application.DTOs.Courses;

public class CourseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
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
}
public class UpdateCourseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}
