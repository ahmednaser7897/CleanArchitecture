using Application.DTOs.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.InterfacesServices;

namespace Application.UseCases.Auth.Queries;

public class LoginQuery(LoginRequestDto dto) : IRequest<BaseApiResponse<LoginResponseDto>>
{
    public LoginRequestDto Dto { get; set; } = dto;
}

public class LoginQueryHandler(IAuthService Repository, ILogger<LoginQueryHandler> Logger) : IRequestHandler<LoginQuery, BaseApiResponse<LoginResponseDto>>
{
    public async Task<BaseApiResponse<LoginResponseDto>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Login in process");
        var value = await Repository.Login(request.Dto);
        return BaseApiResponse<LoginResponseDto>.Success(value, "Loged in successfully");
    }
}


