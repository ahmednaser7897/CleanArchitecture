using Application.DTOs.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.InterfacesServices;

namespace Application.UseCases.Auth.Commands;

public class ReFreshTokenCommand(string refreshToken) : IRequest<BaseApiResponse<TokenResponseDto>>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class ReFreshTokenCommandHandler(IAuthService Repository, ILogger<ReFreshTokenCommandHandler> Logger) : IRequestHandler<ReFreshTokenCommand, BaseApiResponse<TokenResponseDto>>
{
    public async Task<BaseApiResponse<TokenResponseDto>> Handle(ReFreshTokenCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("ReFreshToken in process");
        var value = await Repository.ReFreshToken(request.RefreshToken);
        return BaseApiResponse<TokenResponseDto>.Success(value);
    }
}

