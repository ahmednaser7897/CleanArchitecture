using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeders;

public static class IdentityRolesSeeder
{
    public static async Task SeedIdentityRolesAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();
        // 1. Seed roles only if they don't exist
        if (!roleManager.Roles.Any())
        {
            foreach (var role in UserRoles.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(
                        new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ",
                            result.Errors.Select(e => e.Description));
                        throw new CusttomExption(
                            $"Failed to create role '{role}': {errors}");
                    }
                }
            }
            // 2. Create the admin only if it doesn't exist
            const string adminEmail = "admin@test.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Name = "Super Admin",
                    Bio = "I am the best developer in the world",
                    PhoneKey = "+20",
                    PhoneNumber = "1121362555",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Address = "Cairo",
                    BirthDate = new DateTime(2001, 08, 17),
                };
                var createResult = await userManager.CreateAsync(
                    adminUser, "Admin@123");
                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ",
                        createResult.Errors.Select(e => e.Description));
                    throw new CusttomExption(
                        $"Failed to create admin: {errors}");
                }
            }
            // 3. Assign the Admin role only if it isn't already assigned
            if (!await userManager.IsInRoleAsync(adminUser, UserRoles.Admin))
            {
                var roleResult = await userManager.AddToRoleAsync(
                    adminUser, UserRoles.Admin);
                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ",
                        roleResult.Errors.Select(e => e.Description));
                    throw new CusttomExption(
                        $"Failed to assign Admin role: {errors}");
                }
            }
        }
    }
}