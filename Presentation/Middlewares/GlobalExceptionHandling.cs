using Application.DTOs.Response;
using Domain.Exceptions;
using System.Text.Json;

namespace Presentation.Middlewares;

public class GlobalExceptionHandling(RequestDelegate next, ILogger<GlobalExceptionHandling> logger, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandling> _logger = logger;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError(ex, "Unhandled exception occurred");

        context.Response.ContentType = "application/json";

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        int CustomResponseStatusCode = context.Response.StatusCode;

        if (ex is NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            CustomResponseStatusCode = StatusCodes.Status404NotFound;
        }
        object response = _env.IsDevelopment()
        ?
        new DevelopmentExceptionResponse
        {
            StatusCode = CustomResponseStatusCode,
            Message = ex.Message,
            Status = false,
            StackTrace = ex.StackTrace,
            InnerException = ex.InnerException?.Message,
            InnerStackTrace = ex.InnerException?.StackTrace,
            Path = context.Request.Path,
        }

        : new ProductionExceptionResponse
        {
            StatusCode = CustomResponseStatusCode,
            Message = "An unexpected error occurred. Please contact support.",
            Status = false,
            Path = context.Request.Path,
        };


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}
