using Application.UseCases.Students.Queries;
using Domain.Enums;
using Domain.Specification.Params;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StudentController(IMediator Mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PaginationParams paginationParams)
    {
        var models = await Mediator.Send(new GetAllStudentsQuery(paginationParams));
        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await Mediator.Send(new GetStudentByIdQuery(id));
        return Ok(response);
    }
}
