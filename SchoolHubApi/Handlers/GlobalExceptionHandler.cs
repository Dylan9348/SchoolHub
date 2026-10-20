
using Microsoft.AspNetCore.Diagnostics;

namespace SchoolHubApi.Handlers;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetails) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, Exception exception, CancellationToken ct
    )
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = new() 
            {
                Status = StatusCodes.Status500InternalServerError, 
                Title = "An unexpected error occurred"
            }
        });
    }
}
