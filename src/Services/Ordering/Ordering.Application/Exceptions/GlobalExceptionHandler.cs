
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Ordering.Application.Exceptions;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
        $"An error occurred while processing your request: {exception.Message}");

        httpContext.Response.ContentType = "application/json";

        var exceptionDetails = exception switch
        {
            ValidationException => (Detail: exception.Message, StatusCode: StatusCodes.Status422UnprocessableEntity),

            _ => (Detail: exception.Message, StatusCode: StatusCodes.Status500InternalServerError)
        };

        //if(exception is ValidationException validationException)
        //{
        //    await httpContext.Response.WriteAsJsonAsync(new {validationException.Errors});
        //    return true;
        //}

        return await _problemDetailsService
            .TryWriteAsync(new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                ProblemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.StatusCode,
                    Type = exception.GetType().Name,
                    Title = "An error occurred",
                    Detail = exceptionDetails.Detail
                },
                Exception = exception
            });
    }
}
