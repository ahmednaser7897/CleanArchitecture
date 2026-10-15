
using Infrastructure.Seeders;
using Presentation.IServiceCollectionExtensions;
using Presentation.Middlewares;
using Serilog;

namespace Presentation;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAppDependencyInjection(builder.Configuration, builder.Environment);
        builder.Host.AddSerilogConfiguration();

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            await IdentityRolesSeeder.SeedIdentityRolesAsync(scope.ServiceProvider);
        }
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<GlobalExceptionHandling>();
        app.UseCors(MyAppConstants.CorsAllowAllPolicy);
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSerilogRequestLogging();
        app.Run();
    }
}

