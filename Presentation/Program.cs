
using Infrastructure.Extensions;
using Presentation.Filters;
using Presentation.Middlewares;
using Serilog;

namespace Presentation;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options => options.Filters.Add<ValidateModelAttribute>())
        .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAppDbContext(builder.Configuration);
        builder.Services.AddUnitOfWork();
        builder.Services.AddMediatR();
        builder.Services.AddAppCors();
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

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseSerilogRequestLogging();

        app.Run();
    }
}

