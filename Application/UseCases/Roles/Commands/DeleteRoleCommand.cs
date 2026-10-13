using Application.DTOs.Response;
using Application.InterfacesServices;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Roles.Commands;

public class DeleteRoleCommand : IRequest<BaseApiResponse<bool>>
{
    public required string Id { get; set; }
}
public class DeleteRoleCommandHandler(IRoleService Repository, ILogger<DeleteRoleCommandHandler> Logger) : IRequestHandler<DeleteRoleCommand, BaseApiResponse<bool>>
{
    public async Task<BaseApiResponse<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Deleting role with Id {Id}", request.Id);
        await Repository.DeleteRole(request.Id);
        return BaseApiResponse<bool>.Success(true, "Role deleted successfully");
    }
}