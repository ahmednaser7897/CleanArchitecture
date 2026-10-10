using Application.DTOs.Courses;
using Application.DTOs.Response;
using Application.UseCases.Courses.Commands;
using Application.UseCases.Courses.Queries;
using Domain.Entities;
using Domain.Interfaces.Specification;
using Domain.Interfaces.UnitOfWork;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var courses = await Mediator.Send(new GetAllCoursesQuery(paginationParams));
        return Ok(courses);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetCourseByIdQuery(id));
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(CourseDto dto)
    {
        var result = await Mediator.Send(new CreateCourseCommand() { Dto = dto });
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CourseDto dto)
    {
        var result = await Mediator.Send(new UpdateCourseCommand() { Dto = dto, Id = id });
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await Mediator.Send(new DeleteCourseCommand() { Id = id });
        return Ok(result);
    }
}
