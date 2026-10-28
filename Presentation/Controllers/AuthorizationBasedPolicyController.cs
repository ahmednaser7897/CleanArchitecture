using Application.UseCases.Offices.Queries;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationBasedPolicyController(IMediator Mediator) : ControllerBase
{
    [HttpGet(PolicyNames.MustBeMale)]
    [Authorize(policy: PolicyNames.MustBeMale)]
    public async Task<IActionResult> MustBeMale([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllOfficesQuery(paginationParams));
        return Ok(models);
    }
    [HttpGet(PolicyNames.MustHaveImage)]
    [Authorize(policy: PolicyNames.MustHaveImage)]
    public async Task<IActionResult> MustHaveImage([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllOfficesQuery(paginationParams));
        return Ok(models);
    }

}
