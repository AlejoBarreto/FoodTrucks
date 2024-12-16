using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace FoodTruck.Api.Handlers;
public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception.Message);
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
        await httpContext.Response.WriteAsJsonAsync(new { exception.Message });
        return true;
    }
}

