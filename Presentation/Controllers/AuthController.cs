using Application.DTOs.Accounts;
using Application.UseCases.Auth.Queries;
using MediatR;
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
}
