using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.Accounts;
using Application.DTOs.Options;
using Application.InterfacesServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.ExternalServices;
//this tool create signinKey:
//https://8gwifi.org/jwsgen.jsp
public class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<TokenResponseDto> GenerateJwtToken(List<Claim> climes)
    {
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
        var expiration = jwtSecurityToken.ValidTo;//.ToString("yyyy-MM-ddTHH:mm:ssZ");
        var refresh = "";
        return new TokenResponseDto
        {
            Token = token,
            ExpiresAt = expiration,
            RefreshToken = refresh,
        };
    }
}

