using Application.DTOs.Courses;
using Application.UseCases.Courses.Commands;
using Application.UseCases.Courses.Queries;
using Domain.Enums;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Instructor)]
public class CourseController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllCoursesQuery(paginationParams));
        return Ok(models);
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
