using Domain.Entities;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Presentation.IServiceCollectionExtensions;

public static class IdentityExtentions
{
    public static void AddAppIdentity(this IServiceCollection services)
    {
        //For Authentication
        services.AddIdentity<ApplicationUser, IdentityRole>(
        options =>
        {
            //options.Password
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
        }
        ).AddEntityFrameworkStores<AppDbContext>();
    }
}
