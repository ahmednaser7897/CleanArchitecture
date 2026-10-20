using Application.DTOs.Options;
using Application.DTOs.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Middlewares;
using System.Text.Json;
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

    public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
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
                      ClockSkew = TimeSpan.Zero,
                      //this option is used to validate the issuer signing key
                      ValidateIssuerSigningKey = true,
                      //this option is used to set the issuer signing key
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),

                  };
                  // Keep your existing TokenValidationParameters here.
                  options.Events = new JwtBearerEvents
                  {
                      OnChallenge = async context =>
                      {
                          // Prevent the default empty 401 response.
                          context.HandleResponse();
                          context.Response.StatusCode =
                              StatusCodes.Status401Unauthorized;
                          context.Response.ContentType = "application/json";
                          var response = GetAuthResponse(
                            IsDevelopment: env.IsDevelopment(),
                            message: "Unauthorized. A valid access token is required.",
                            path: context.Request.Path.Value ?? "",
                            statusCode: StatusCodes.Status401Unauthorized
                          );
                          await context.Response.WriteAsync(
                              JsonSerializer.Serialize(response, GlobalExceptionHandling.jsonOptions)
                          );
                      },

                      OnForbidden = async context =>
                      {
                          context.Response.StatusCode =
                              StatusCodes.Status403Forbidden;
                          context.Response.ContentType = "application/json";
                          var response = GetAuthResponse(
                            IsDevelopment: env.IsDevelopment(),
                            message: "You do not have permission to access this resource.",
                            path: context.Request.Path.Value ?? "",
                            statusCode: StatusCodes.Status403Forbidden
                          );
                          await context.Response.WriteAsync(
                              JsonSerializer.Serialize(response)
                          );
                      }
                  };
              }
          );
    }
    public static BaseApiResponse GetAuthResponse(bool IsDevelopment, string message, string path, int statusCode)
    {
        return IsDevelopment
         ?
         new DevelopmentExceptionResponse
         {
             StatusCode = statusCode,
             Message = message,
             Status = false,
             StackTrace = "",
             InnerException = "",
             InnerStackTrace = "",
             Path = path,
         }

         : new ProductionExceptionResponse
         {
             StatusCode = statusCode,
             Message = message,
             Status = false,
             Path = path,
         };
    }
}
