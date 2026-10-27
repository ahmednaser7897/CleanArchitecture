using Application.DTOs.Accounts;
using Application.DTOs.Response;
using Application.InterfacesServices;
using MediatR;
using Microsoft.Extensions.Logging;
namespace Application.UseCases.Auth.Commands;

public class ReVokeTokenCommand(string refreshToken) : IRequest<BaseApiResponse<bool>>
{
    public string RefreshToken { get; set; } = refreshToken;
}

public class ReVokeTokenQueryHandler(IJwtService Repository, ILogger<ReVokeTokenQueryHandler> Logger) : IRequestHandler<ReVokeTokenCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(ReVokeTokenCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("ReFreshToken in process");
        var value = await Repository.ReVokeToken(request.RefreshToken);
        return BaseApiResponse<bool>.Success(value);
    }
}


