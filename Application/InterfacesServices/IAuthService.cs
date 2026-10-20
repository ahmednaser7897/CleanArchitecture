using Application.DTOs.Accounts;
namespace Application.InterfacesServices;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto Dto);
    Task<string?> Register(RegisterRequestDto Dto, string role);
    Task<TokenResponseDto> ReFreshToken(string refreshToken);
    Task<bool> ReVokeToken(string refreshToken);
    // Task<bool> Logout();
}
