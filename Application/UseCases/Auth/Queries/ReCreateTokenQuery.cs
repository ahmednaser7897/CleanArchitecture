using Application.DTOs.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.InterfacesServices;

namespace Application.UseCases.Auth.Queries;

public class ReFreshTokenQuery(string refreshToken) : IRequest<BaseApiResponse<TokenResponseDto>>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class ReFreshTokenQueryHandler(IAuthService Repository, ILogger<ReFreshTokenQueryHandler> Logger) : IRequestHandler<ReFreshTokenQuery, BaseApiResponse<TokenResponseDto>>
{
    public async Task<BaseApiResponse<TokenResponseDto>> Handle(ReFreshTokenQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("ReFreshToken in process");
        var value = await Repository.ReFreshToken(request.RefreshToken);
        return BaseApiResponse<TokenResponseDto>.Success(value);
    }
}

