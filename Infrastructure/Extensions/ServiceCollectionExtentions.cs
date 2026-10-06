using Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.UnitOfWork;
using Application.UseCases.Courses.Commands;
using Serilog;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtentions
{
    public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
                  options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
              );
    }
    public static void AddAppCors(this IServiceCollection services)
    {
        // this line add the cors policy to the container and this enable cross-origin requests
        // from the frontend (Angular in this case) to the backend (ASP.NET Core API).
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
            });
        });
    }
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, BaseUnitOfWork>();
    }
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCourseCommand).Assembly));
    }
    public static void AddSerilogConfiguration(this ConfigureHostBuilder hostBuilder)
    {
        //Add this package Serilog.AspNetCore
        //Add this lins on the Program.cs to use the serilog configuration
        //builder.Host.AddSerilogConfiguration();
        //app.UseSerilogRequestLogging();
        //now we can change this based on requirements from appsettings.Development.json and appsettings.Production.json
        hostBuilder.UseSerilog(
          (context, configuration)
          => configuration.ReadFrom.Configuration(context.Configuration)
      //   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
      //   .WriteTo.File("logs/{Date}.txt", rollingInterval: RollingInterval.Day)
      //   .WriteTo.Console(
      //       outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
      //   )
      );
    }

}