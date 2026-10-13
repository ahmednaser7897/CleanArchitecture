using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Account.Queries;
using Application.UseCases.Account.Commands;


namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = UserRoles.Admin)]
public class RolesController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Roles = await Mediator.Send(new GetAllRolesQuery());
        return Ok(Roles);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await Mediator.Send(new GetRoleByIdQuery(id));
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string name)
    {
        var result = await Mediator.Send(new CreateRoleCommand() { Name = name });
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] string id, [FromBody] string name)
    {
        var result = await Mediator.Send(new UpdateRoleCommand() { Id = id, Name = name });
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        var result = await Mediator.Send(new DeleteRoleCommand() { Id = id });
        return Ok(result);
    }
}
