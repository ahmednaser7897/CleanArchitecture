using Application.DTOs.Response;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.DTOs.Accounts;
using Application.InterfacesServices;

namespace Application.UseCases.Roles.Queries;

public class GetAllRolesQuery() : IRequest<BaseApiResponse<List<RoleDto>>>
{
}

public class GetAllRolesQueryHandler(IRoleService Repository, ILogger<GetAllRolesQueryHandler> Logger) : IRequestHandler<GetAllRolesQuery, BaseApiResponse<List<RoleDto>>>
{
    public async Task<BaseApiResponse<List<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting All Roless");
        var value = await Repository.GetRoles();
        return BaseApiResponse<List<RoleDto>>.Success(value, "Role updated successfully");
    }
}


