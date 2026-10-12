using Application.DTOs.Options;
using Domain.AppConstants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Presentation.IServiceCollectionExtensions;
/*
Add this to appsettings.Development.json or appsettings.Production.json if it doesn't exist 

"JwtConfig": {
    "key": "ThisIsASecretKeyForDevelopment123456789",
    "issuer": "http://localhost:5131",
    "audience": "http://localhost:5131",
    "LifeTime":30
}

*/
public static class JWTExtentions
{
    public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(MyAppConstants.JwtConfigSectionName).Get<JwtOptions>();
        //for JWT Authentication 
        //This configuration is applied when the application starts and is used by the application to authenticate requests.
        services.AddAuthentication(
          options =>
          {
              //This is the default authentication scheme that will be used to authenticate requests.
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              //This is the default challenge scheme that will be used to challenge requests.
              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              //This is the default scheme that will be used to authenticate requests.
              options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
          }
          ).AddJwtBearer(
              options =>
              {
                  //this option is used to save the token in the client side
                  options.SaveToken = true;
                  //this option is used to require the https metadata
                  options.RequireHttpsMetadata = false;
                  //this option is used to validate the token
                  options.TokenValidationParameters =
                  new TokenValidationParameters
                  {
                      //this option is used to validate the issuer
                      ValidateIssuer = true,
                      //this option is used to set the issuer
                      ValidIssuer = jwtOptions!.Issuer,
                      //this option is used to validate the audience
                      ValidateAudience = true,
                      //this option is used to set the audience
                      ValidAudience = jwtOptions.Audience,
                      //this option is used to validate the lifetime
                      ValidateLifetime = true,
                      //this option is used to set the lifetime
                      ClockSkew = TimeSpan.FromMinutes(jwtOptions.LifeTime),
                      //this option is used to validate the issuer signing key
                      ValidateIssuerSigningKey = true,
                      //this option is used to set the issuer signing key
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                  };
              }
          );
    }


}
