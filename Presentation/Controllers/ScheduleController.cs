using Application.DTOs.Schedules;
using Application.UseCases.Schedules.Commands;
using Application.UseCases.Schedules.Queries;
using Domain.Enums;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Owner)]
public class ScheduleController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllSchedulesQuery(paginationParams));
        return Ok(models);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetScheduleByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ScheduleDto dto)
    {
        var result = await Mediator.Send(new CreateScheduleCommand() { Dto = dto });
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ScheduleDto dto)
    {
        var result = await Mediator.Send(new UpdateScheduleCommand() { Dto = dto, Id = id });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await Mediator.Send(new DeleteScheduleCommand() { Id = id });
        return Ok(result);
    }
}
