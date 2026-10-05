using Application.DTOs.Courses;
using Domain.Entities;
using Domain.Interfaces.Specification;
using Domain.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(IUnitOfWork UnitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var courses = (await UnitOfWork.Courses.GetAllWithSpec(new GetCourseSpec()))
                      .Select(c => CourseDto.FromEntit(c));
        return Ok(courses);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await UnitOfWork.Courses.GetByIdWithSpec(new GetCourseSpec(), id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(CourseDto.FromEntit(course));
    }
    [HttpPost]
    public async Task<IActionResult> Post(AddCourseDto courseDto)
    {
        var course = new Course
        {
            Name = courseDto.Name,
            Price = courseDto.Price,
        };
        await UnitOfWork.Courses.Add(course);
        return Ok(CourseDto.FromEntit(course));
    }
    [HttpPut]
    public async Task<IActionResult> Put(UpdateCourseDto courseDto)
    {
        var course = await UnitOfWork.Courses.GetById(courseDto.Id);
        if (course == null)
        {
            return NotFound();
        }
        course.Name = courseDto.Name;
        course.Price = courseDto.Price;
        await UnitOfWork.Courses.Update(course);
        return Ok(CourseDto.FromEntit(course));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await UnitOfWork.Courses.Remove(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
public class GetCourseSpec : BaseSpecifications<Course, int>
{
    public GetCourseSpec() : base()
    {
        AddInclude(c => c.Sections);
    }
}

