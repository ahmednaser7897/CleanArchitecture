using Application.DTOs.Offices;
using Application.UseCases.Offices.Commands;
using Application.UseCases.Offices.Queries;
using Domain.Enums;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = UserRoles.Admin)]
public class OfficeController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllOfficesQuery(paginationParams));
        return Ok(models);
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetOfficeByIdQuery(id));
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Post(OfficeDto dto)
    {
        var result = await Mediator.Send(new CreateOfficeCommand() { Dto = dto });
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] OfficeDto dto)
    {
        var result = await Mediator.Send(new UpdateOfficeCommand() { Dto = dto, Id = id });
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await Mediator.Send(new DeleteOfficeCommand() { Id = id });
        return Ok(result);
    }
}
