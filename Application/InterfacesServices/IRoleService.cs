using Application.DTOs.Accounts;

namespace Application.InterfacesServices;

public interface IRoleService
{
    Task<bool> AddRole(string role);
    Task<List<RoleDto>> GetRoles();
    Task<RoleDto> GetRoleById(string id);
    Task<bool> DeleteRole(string id);
    Task<bool> UpdateRole(string id, string role);
}