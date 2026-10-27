using Application.DTOs.Accounts;
using Application.UseCases.Auth.Commands;
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
        var result = await Mediator.Send(new RegisterCommand(dto, UserRoles.Admin));
        return Ok(result);
    }
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("registerInstructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterRequestDto dto, int? officeId = null)
    {
        var result = await Mediator.Send(new RegisterCommand(dto, UserRoles.Instructor, officeId));
        return Ok(result);
    }
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("registerStudent")]
    public async Task<IActionResult> RegisterStudent(RegisterRequestDto dto)
    {
        var result = await Mediator.Send(new RegisterCommand(dto, UserRoles.Student));
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("reFreshToken")]
    public async Task<IActionResult> ReFreshToken([FromBody] ReCreateTokenRequestDto dto)
    {
        var result = await Mediator.Send(new ReFreshTokenCommand(dto.RefreshToken));
        return Ok(result);
    }
    [AllowAnonymous]
    [HttpPost("reVokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody] ReCreateTokenRequestDto dto)
    {
        var result = await Mediator.Send(new ReVokeTokenCommand(dto.RefreshToken));
        return Ok(result);
    }
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("deleteStudent")]
    public async Task<IActionResult> DeleteStudent([FromQuery] string userId)
    {
        var result = await Mediator.Send(new DeleteUserCommand(userId: userId, role: UserRoles.Student)
       );
        return Ok(result);
    }
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("deleteInstructor")]
    public async Task<IActionResult> DeleteInstructor([FromQuery] string userId)
    {
        var result = await Mediator.Send(new DeleteUserCommand(userId: userId, role: UserRoles.Instructor)
       );
        return Ok(result);
    }
}
