using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace Infrastructure.Extensions;
/*
Add this to on of the Swagger methods in Program.cs
builder.Services.AddSwaggerWithNoAuth();
builder.Services.AddSwaggerWithJWTAuth();
builder.Services.AddSwaggerWithBasicAuth();

And Add this in Program.cs before app.UseAuthorization();
if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
*/

public static class SwaggerExtentions
{
    public static void AddSwaggerWithNoAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }
    public static void AddSwaggerWithJWTAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token"
            });

            options.AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] =
                        []
                });
        });
    }
    public static void AddSwaggerWithBasicAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Basic",
                In = ParameterLocation.Header,
                Description = "Enter your username and password"
            });

            options.AddSecurityRequirement(document =>
                new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Basic", document)] =
                        []
                });
        });
    }
}