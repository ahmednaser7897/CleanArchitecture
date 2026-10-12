
using Domain.Entities;
using Presentation.Filters;
using Presentation.IServiceCollectionExtensions;
using Presentation.Middlewares;
using Serilog;

namespace Presentation;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddAppDependencyInjection(builder.Configuration);

        builder.Host.AddSerilogConfiguration();
        var app = builder.Build();
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
        app.UseAuthorization();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSerilogRequestLogging();
        app.Run();
    }
}

