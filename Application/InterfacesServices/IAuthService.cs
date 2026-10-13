using Application.DTOs.Accounts;
namespace Application.InterfacesServices;

public interface IAuthService
{
    Task<LoginResponseDto> Login(LoginRequestDto Dto);
    // Task<bool> RegisterAsync(RegisterDTO Dto);
    // Task<bool> Logout();
}
