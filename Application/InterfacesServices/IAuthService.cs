using Application.DTOs.Accounts;
namespace Application.InterfacesServices;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto Dto);
    Task<string?> Register(RegisterRequestDto Dto, string role);
    // Task<bool> Logout();
}
