using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authorization;

public class AppUserClaimsPrincipalFactory(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IOptions<IdentityOptions> options
    ) : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>(userManager, roleManager, options)
{
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        //Generate a Claims
        identity.AddClaim(new(ClaimTypes.NameIdentifier, user.Id));
        identity.AddClaim(new(ClaimTypes.Email, user.Email ?? ""));
        identity.AddClaim(new(ClaimTypes.Name, user.Name ?? ""));
        identity.AddClaim(new(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""));
        identity.AddClaim(new("PhoneKey", user.PhoneKey ?? ""));
        identity.AddClaim(new(ClaimTypes.Gender, user.Gender.ToString() ?? ""));
        identity.AddClaim(new(ClaimTypes.DateOfBirth, user.BirthDate.ToString() ?? ""));
        identity.AddClaim(new("Bio", user.Bio ?? ""));
        identity.AddClaim(new("ImageUrl", user.ImageUrl ?? ""));
        return identity;
    }
}