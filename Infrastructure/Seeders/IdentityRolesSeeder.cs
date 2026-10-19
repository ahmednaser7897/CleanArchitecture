using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Seeders;

public static class IdentityRolesSeeder
{
    public static async Task SeedIdentityRolesAsync(
        IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var services = scope.ServiceProvider;

        var dbContext = services.GetRequiredService<AppDbContext>();

        var roleManager =
            services.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager =
            services.GetRequiredService<UserManager<ApplicationUser>>();

        await using var transaction =
            await dbContext.Database.BeginTransactionAsync();

        try
        {
            if (roleManager.Roles.Any())
                return;
            // 1. Create roles if they don't exist
            foreach (var role in UserRoles.Roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                    continue;

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

            // 2. Create the owner if it doesn't exist
            const string email = "owner@test.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    Name = "Owner",
                    Bio = "I am the owner of the platform",
                    PhoneKey = "+20",
                    PhoneNumber = "1121362555",
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Gender = Gender.Male,
                    BirthDate = new DateTime(2001, 08, 17)
                };

                var createResult = await userManager.CreateAsync(
                    user, "Owner@123");

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(", ",
                        createResult.Errors.Select(e => e.Description));

                    throw new CusttomExption(
                        $"Failed to create Owner: {errors}");
                }
            }

            // 3. Assign the Owner role if it isn't assigned
            if (!await userManager.IsInRoleAsync(user, UserRoles.Owner))
            {
                var roleResult = await userManager.AddToRoleAsync(
                    user, UserRoles.Owner);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(", ",
                        roleResult.Errors.Select(e => e.Description));

                    throw new CusttomExption(
                        $"Failed to assign Owner role: {errors}");
                }
            }

            // 4. Commit only after every operation succeeds
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
