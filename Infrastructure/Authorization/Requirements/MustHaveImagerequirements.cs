using System.Security.Claims;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace Infrastructure.Authorization.Requirements;

public class MustHaveImagerequirements : IAuthorizationRequirement;
public class MustHaveImagerequirementsHandeler(UserManager<ApplicationUser> userManager) : AuthorizationHandler<MustHaveImagerequirements>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustHaveImagerequirements requirement)
    {
        Console.WriteLine("======================================");
        Console.WriteLine("MustHaveImage");
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            Console.WriteLine("userId is null");
            context.Fail();
            return;
        }
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            Console.WriteLine("user is null");
            context.Fail();
            return;
        }
        var image = user.ImageUrl ?? "";
        if (string.IsNullOrWhiteSpace(image))
        {
            Console.WriteLine("image is null or empty");
            context.Fail();
        }
        else
        {
            Console.WriteLine("image is not null or empty");
            context.Succeed(requirement);
        }
    }
}