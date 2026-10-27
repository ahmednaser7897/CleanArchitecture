using System.Security.Claims;
using Application.DTOs.Accounts;
using Domain.Entities;

namespace Application.InterfacesServices;

public interface IJwtService
{
    Task<TokenResponseDto> GenerateJwtToken(ApplicationUser user);
    Task<TokenResponseDto> ReFreshToken(string refreshToken);
    Task<bool> ReVokeToken(string refreshToken);
}

