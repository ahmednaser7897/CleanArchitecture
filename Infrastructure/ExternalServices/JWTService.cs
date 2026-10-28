using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.Accounts;
using Application.DTOs.Options;
using Application.InterfacesServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ExternalServices;
//this tool create signinKey:
//https://8gwifi.org/jwsgen.jsp
public class JwtService(IOptions<JwtOptions> jwtOptions, UserManager<ApplicationUser> userManager) : IJwtService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<TokenResponseDto> GenerateJwtToken(ApplicationUser user)
    {
        //Generate a Claims
        var climes = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.Name ?? ""),
            new(ClaimTypes.Gender, user.Gender.ToString() ?? "")
        };
        //get user roles and add it to claims
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            climes.Add(new Claim(ClaimTypes.Role, role));
        }
        //Generate Token id clime(Jti) that will be changed each login
        climes.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //Generate signing Credentials
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
        //create the token
        var jwtSecurityToken = new JwtSecurityToken(
            //issuer: the server that create the token
            issuer: _jwtOptions.Issuer,
            //audience: the server that will accept the token
            audience: _jwtOptions.Audience,
            //claims: the claims that will be added to the token
            claims: climes,
            //expires: the expiration time of the token
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTime),
            //signingCredentials: the credentials that will be used to sign the token
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        var refresh = await GenerateRefreshToken(user);
        return new TokenResponseDto
        {
            Token = token,
            RefreshTokenExpiresOn = refresh.ExpiresAt,
            RefreshToken = refresh.Token,
        };
    }

    public async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user)
    {
        if (user.RefreshTokens.Any(t => t.IsActive))
        {
            var activeToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
            if (activeToken != null)
            {
                return activeToken;
            }
        }
        var randomNumber = new byte[32];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifeTime),
        };
        user.RefreshTokens.Add(refreshToken);
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new CusttomExption("Failed to generate refresh token");
        }
        return refreshToken;
    }
    public async Task<TokenResponseDto> ReFreshToken(string refreshToken)
    {
        var user = await _userManager.Users.Include(t => t.RefreshTokens)
        .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken)) ??
        throw new CusttomExption("Invalid refresh token");
        var activeToken = user.RefreshTokens.Single(t => t.Token == refreshToken);
        if (!activeToken.IsActive)
        {
            throw new CusttomExption("Refresh token has expired");
        }
        //activeToken.RevokedAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        //Generate a Token
        return await GenerateJwtToken(user);
    }

    public async Task<bool> ReVokeToken(string refreshToken)
    {
        var user = await _userManager.Users.Include(t => t.RefreshTokens)
        .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken)) ??
        throw new CusttomExption("Invalid refresh token");
        var activeToken = user.RefreshTokens.Single(t => t.Token == refreshToken);
        if (!activeToken.IsActive)
        {
            throw new CusttomExption("Refresh token has expired");
        }
        activeToken.RevokedAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        return true;
    }
}

