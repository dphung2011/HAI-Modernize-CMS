using System.Net;
using System.Text.Json;

namespace CMS.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ErrorHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

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
        _logger.LogError(ex, "An unhandled exception occurred.");

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";
        
        // Customize the response based on exception type
        if (ex is KeyNotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (ex is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = ex.Message;
        }
        else if (ex is InvalidOperationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else if (ex is ArgumentException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }

        var result = JsonSerializer.Serialize(new 
        { 
            error = message,
            // Include stack trace only in development
            stackTrace = _environment.IsDevelopment() ? ex.StackTrace : null
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(result);
    }
}