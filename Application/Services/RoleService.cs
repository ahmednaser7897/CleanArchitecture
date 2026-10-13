using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.InterfacesServices;
using Application.DTOs.Accounts;

namespace Application.Services;

public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
{
    public async Task<bool> AddRole(string role)
    {
        var roleEntity = await roleManager.RoleExistsAsync(role);
        if (roleEntity)
            throw new CusttomExption("Role already exists");
        var result = await roleManager.CreateAsync(new IdentityRole
        {
            Name = role,
            NormalizedName = role.ToUpper(),
        });
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CusttomExption($"Failed to create role: {errors}");
        }
        return true;
    }

    public async Task<bool> DeleteRole(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role == null)
            throw new CusttomExption("Role not found");
        var result = await roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CusttomExption($"Failed to delete role: {errors}");
        }
        return true;
    }

    public async Task<RoleDto> GetRoleById(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role == null)
            throw new CusttomExption("Role not found");
        return new RoleDto() { Id = role.Id, Name = role.Name! };
    }

    public async Task<List<RoleDto>> GetRoles()
    {
        var roles = await roleManager.Roles.ToListAsync();
        return [.. roles.Select(r => new RoleDto() { Id = r.Id, Name = r.Name! })];
    }

    public async Task<bool> UpdateRole(string id, string role)
    {
        var roleEntity = await roleManager.FindByIdAsync(id);
        if (roleEntity == null)
            throw new CusttomExption("Role not found");
        roleEntity.Name = role;
        roleEntity.NormalizedName = role.ToUpper();
        var result = await roleManager.UpdateAsync(roleEntity);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new CusttomExption($"Failed to update role: {errors}");
        }
        return true;
    }
}