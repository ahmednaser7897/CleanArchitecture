using Application.InterfacesServices;
using Application.DTOs.Accounts;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Domain.Exceptions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IJwtService _jwtService = jwtService;
    public async Task<string?> Register(RegisterRequestDto Dto, string role)
    {
        //check if the user exists
        var user = await _userManager.FindByEmailAsync(Dto.Email);
        if (user != null)
        {
            throw new CusttomExption("User already exists");
        }
        //check if the phone number exists
        var phone = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == Dto.Phone);
        if (phone != null)
        {
            throw new CusttomExption("Phone number already exists");
        }

        //create a new user
        user = new ApplicationUser
        {
            UserName = Dto.Email,
            Email = Dto.Email,
            Name = Dto.Name,
            PhoneNumber = Dto.Phone,
            PhoneKey = Dto.PhoneKey,
            ImageUrl = Dto.ImageUrl,
            BirthDate = Dto.BirthDate,
            Bio = Dto.Bio,
            Gender = Dto.Gender
        };

        //create the user in the database
        var result = await _userManager.CreateAsync(user, Dto.Password);

        //if the user is created successfully
        if (!result.Succeeded)
        {
            throw new CusttomExption("User creation failed");
        }
        //check if role exists
        var roleExists = await _roleManager.RoleExistsAsync(role);
        if (!roleExists)
        {
            throw new CusttomExption($"Role {role} is not found ");
        }
        //add user to a role
        await _userManager.AddToRoleAsync(user, role);

        return user.Id;
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto Dto)
    {
        //check if the user exists
        var user = await _userManager.FindByEmailAsync(Dto.Email)
        ?? throw new CusttomExption("Invalid user name or password");

        //check if the password is correct
        if (!await _userManager.CheckPasswordAsync(user, Dto.Password))
        {
            throw new CusttomExption("Invalid user name or password");
        }

        //Generate a Claims
        var climes = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Email, user.Email ?? ""),
                new (ClaimTypes.Name, user.UserName ?? "")
            };
        //get user roles and add it to claims
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            climes.Add(new Claim(ClaimTypes.Role, role));
        }

        //Generate a Token
        var tokenResponse = await _jwtService.GenerateJwtToken(climes);

        return new LoginResponseDto
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            Name = user.UserName ?? "",
            PhoneKey = user.PhoneKey,
            PhoneNumber = user.PhoneNumber ?? "",
            Gender = user.Gender ?? Gender.Male,
            ImageUrl = user.ImageUrl ?? "",
            BirthDate = user.BirthDate,
            Roles = roles,
            TokenResponse = tokenResponse
        };
    }
}
