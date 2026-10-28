using Application.DTOs.Sections;
using Application.UseCases.Sections.Commands;
using Application.UseCases.Sections.Queries;
using Domain.Enums;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = UserRoles.Admin)]
public class SectionController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllSectionsQuery(paginationParams));
        return Ok(models);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetSectionByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(SectionDto dto)
    {
        var result = await Mediator.Send(new CreateSectionCommand() { Dto = dto });
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] SectionDto dto)
    {
        var result = await Mediator.Send(new UpdateSectionCommand() { Dto = dto, Id = id });
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        var result = await Mediator.Send(new DeleteSectionCommand() { Id = id });
        return Ok(result);
    }
}
