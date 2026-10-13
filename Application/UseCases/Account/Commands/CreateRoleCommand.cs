using Application.DTOs.Response;
using Application.InterfacesServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Account.Commands;

public class CreateRoleCommand : IRequest<BaseApiResponse<bool>>
{
    public required string Name { get; set; }
}
public class CreateRoleCommandHandler(IRoleRepository Repository, ILogger<CreateRoleCommandHandler> Logger) : IRequestHandler<CreateRoleCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Creating Role With Name {Name}", request.Name);
        await Repository.AddRole(request.Name);
        return BaseApiResponse<bool>.Success(true, "Role created successfully");
    }
}