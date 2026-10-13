using Application.DTOs.Response;
using Application.InterfacesServices;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Roles.Queries;

public class GetRoleByIdQuery(string id) : IRequest<BaseApiResponse<RoleDto>>
{
    public string Id { get; set; } = id;
}

public class GetRoleByIdQueryHandler(IRoleService Repository, ILogger<GetRoleByIdQueryHandler> Logger) : IRequestHandler<GetRoleByIdQuery, BaseApiResponse<RoleDto>>
{
    public async Task<BaseApiResponse<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting Role With Id {Id}", request.Id);
        var value = await Repository.GetRoleById(request.Id);
        return BaseApiResponse<RoleDto>.Success(value, "Role Found Successfully");
    }
}


