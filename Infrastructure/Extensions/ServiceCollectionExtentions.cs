using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.UnitOfWork;
using Infrastructure.UnitOfWork;

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

}