using Application.InterfacesServices;
using Application.DTOs.Accounts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Domain.Exceptions;
using System.Security.Claims;

namespace Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, IJwtService jwtService) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<LoginResponseDto> Login(LoginRequestDto Dto)
    {
        //check if the user exists
        var user = await _userManager.FindByEmailAsync(Dto.Email)
        ?? throw new CusttomExption("Invalid user name or password");

        //check if the password is correct
        if (!await _userManager.CheckPasswordAsync(user, Dto.Password))
        {
            throw new CusttomExption("Invalid user name or password");
        }

        //Generate a Claims
        var climes = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Email, user.Email ?? ""),
                new (ClaimTypes.Name, user.UserName ?? "")
            };
        //get user roles and add it to claims
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            climes.Add(new Claim(ClaimTypes.Role, role));
        }

        //Generate a Token
        var tokenResponse = await _jwtService.GenerateJwtToken(climes);

        return new LoginResponseDto
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            Name = user.UserName ?? "",
            Roles = roles,
            TokenResponse = tokenResponse
        };
    }
}
