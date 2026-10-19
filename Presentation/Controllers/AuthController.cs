using Application.DTOs.Accounts;
using Application.UseCases.Auth.Queries;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator Mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var result = await Mediator.Send(new LoginQuery(dto));
        return Ok(result);
    }
    [HttpPost("registerAdmin")]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> RegisterAdmin(RegisterRequestDto dto)
    {
        var result = await Mediator.Send(new RegisterQuery(dto, UserRoles.Admin));
        return Ok(result);
    }
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("registerInstructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterRequestDto dto, int? officeId = null)
    {
        var result = await Mediator.Send(new RegisterQuery(dto, UserRoles.Instructor, officeId));
        return Ok(result);
    }
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Instructor)]
    [HttpPost("registerStudent")]
    public async Task<IActionResult> RegisterStudent(RegisterRequestDto dto)
    {
        var result = await Mediator.Send(new RegisterQuery(dto, UserRoles.Student));
        return Ok(result);
    }
}
