using Application.DTOs.Courses;
using Application.UseCases.Courses.Commands;
using Application.UseCases.Courses.Queries;
using Domain.Entities;
using Domain.Interfaces.Specification;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var courses = await Mediator.Send(new GetAllCoursesQuery());
        return Ok(courses);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await Mediator.Send(new GetCourseByIdQuery(id));
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }
    [HttpPost]
    public async Task<IActionResult> Post(CourseDto courseDto)
    {
        var result = await Mediator.Send(new CreateCourseCommand() { CourseDto = courseDto });
        if (!result) return BadRequest();
        return Ok("Created successfully");
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CourseDto courseDto)
    {
        var result = await Mediator.Send(new UpdateCourseCommand() { CourseDto = courseDto, Id = id });
        if (!result) return NotFound("The Course is not found or faild to update");
        return Ok("Updated successfully");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await Mediator.Send(new DeleteCourseCommand() { Id = id });
        if (!result)
        {
            return NotFound("The Course is not found or faild to update");
        }
        return Ok("Deleted successfully");
    }
}
