using System.Security.Claims;
using Application.DTOs.Accounts;
using Domain.Entities;

namespace Application.InterfacesServices;

public interface IJwtService
{
    /// <summary>Issues a JWT for the user with no tenant context. Used for platform-only users
    /// and as the base path during single-tenant logins.</summary>
    Task<TokenResponseDto> GenerateJwtToken(ApplicationUser user);
    Task<TokenResponseDto> ReFreshToken(string refreshToken);
    Task<bool> ReVokeToken(string refreshToken);
    //Task<TokenResponseDto> GenerateRefreshToken(List<Claim> climes);

    // /// <summary>Issues a JWT bound to the given membership — emits tenant_id, membership_id,
    // /// scope, branch, owner/platform flags. Pass null to fall back to the no-tenant overload.</summary>
    // Task<TokenResponseDto> GenerateJwtToken(AppUser user, UserTenantMembership? membership);

    // Task<string> GenerateRefreshToken(string ipAddress, string UserId);
    // void RemoveTokenCookie();
}

