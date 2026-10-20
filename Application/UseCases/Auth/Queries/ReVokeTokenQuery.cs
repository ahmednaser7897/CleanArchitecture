using Application.DTOs.Accounts;
using Application.DTOs.Response;
using Application.InterfacesServices;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.UseCases.Auth.Queries;

public class ReVokeTokenQuery(string refreshToken) : IRequest<BaseApiResponse<bool>>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class ReVokeTokenQueryHandler(IAuthService Repository, ILogger<ReVokeTokenQueryHandler> Logger) : IRequestHandler<ReVokeTokenQuery, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(ReVokeTokenQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("ReFreshToken in process");
        var value = await Repository.ReVokeToken(request.RefreshToken);
        return BaseApiResponse<bool>.Success(value);
    }
}


