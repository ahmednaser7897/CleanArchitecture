using Application.DTOs.Enrollments;
using Application.UseCases.Enrollments.Commands;
using Application.UseCases.Enrollments.Queries;
using Domain.Enums;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Instructor)]
public class EnrollmentController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllEnrollmentsQuery(paginationParams));
        return Ok(models);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetEnrollmentByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(EnrollmentDto dto)
    {
        var result = await Mediator.Send(new CreateEnrollmentCommand() { Dto = dto });
        return Ok(result);
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EnrollmentDto dto)
    // {
    //     var result = await Mediator.Send(new UpdateEnrollmentCommand() { Dto = dto, Id = id });
    //     return Ok(result);
    // }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await Mediator.Send(new DeleteEnrollmentCommand() { Id = id });
        return Ok(result);
    }
}
