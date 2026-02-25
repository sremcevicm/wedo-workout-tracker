using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Domain.Exceptions;

namespace WorkoutTracker.API.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title, errors) = exception switch
        {
            ValidationException ve => (
                StatusCodes.Status400BadRequest,
                "Validation failed",
                ve.Errors.Select(e => e.ErrorMessage).ToArray()
            ),
            NotFoundException => (
                StatusCodes.Status404NotFound,
                exception.Message,
                Array.Empty<string>()
            ),
            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                Array.Empty<string>()
            )
        };

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
        };

        if (errors.Length > 0)
            problem.Extensions["errors"] = errors;

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}
