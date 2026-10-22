using Application.UseCases.Instructors.Queries;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Instructor)]
public class InstructorController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllInstructorsQuery(paginationParams));
        return Ok(models);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetInstructorByIdQuery(id));
        return Ok(response);
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> Put([FromRoute] int id, [FromBody] InstructorDto dto)
    // {
    //     var result = await Mediator.Send(new UpdateInstructorCommand() { Dto = dto, Id = id });
    //     return Ok(result);
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Remove(int id)
    // {
    //     var result = await Mediator.Send(new DeleteInstructorCommand() { Id = id });
    //     return Ok(result);
    // }
}
