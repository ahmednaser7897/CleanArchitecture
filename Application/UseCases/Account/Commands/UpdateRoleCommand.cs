using Application.DTOs.Response;
using Application.InterfacesServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Account.Commands;

public class UpdateRoleCommand : IRequest<BaseApiResponse<bool>>
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}
public class UpdateRoleCommandHandler(IRoleRepository Repository, ILogger<UpdateRoleCommandHandler> Logger) : IRequestHandler<UpdateRoleCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Updating role with Id {Id}", request.Id);
        await Repository.UpdateRole(request.Id, request.Name);
        return BaseApiResponse<bool>.Success(true, "Role updated successfully");
    }
}