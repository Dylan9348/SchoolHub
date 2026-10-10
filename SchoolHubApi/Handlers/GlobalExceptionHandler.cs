
using Microsoft.AspNetCore.Diagnostics;
using SchoolHubApi.Exceptions;

namespace SchoolHubApi.Handlers;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetails) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken ct
    )
    {
        var (status, title) = exception switch
        {
            BadRequestException => (400, "Bad Request"),
            NotFoundException => (404, "Not Found"),
            ConflicException => (409, "Conflict"),
            _ => (500, "An unexpected error occurred")
        };

        context.Response.StatusCode = status;

        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = new() 
            {
                Status = status, 
                Title = title,
                Detail = status == 500 ? null : exception.Message
            }
        });
    }
}
