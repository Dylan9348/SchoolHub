
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SchoolHubApi.Results;

namespace SchoolHubApi.Extensions;

public static class ResultExtension
{
    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        var error = result.Errors.FirstOrDefault();

        if (error is null)
            return new ObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred"
            })
            { StatusCode = StatusCodes.Status500InternalServerError };

        return error.ToActionResult();
    }

    public static IActionResult ToActionResult<TValue>(this Result<TValue> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        return result.ToResult().ToActionResult();
    }

    public static IActionResult ToActionResult(this IError error)
    {
        var problem = error.ToProblemDetails();

        return new ObjectResult(problem) { StatusCode = problem.Status };
    }

    public static int ToStatusCode(this IError error) => error switch
    {
        NotFoundError => StatusCodes.Status404NotFound,
        ForbiddenError => StatusCodes.Status403Forbidden,
        UnauthorizedError => StatusCodes.Status401Unauthorized,
        ConflictError => StatusCodes.Status409Conflict,
        AlreadyTakenError => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status400BadRequest
    };

    public static string ToTitle(this IError error) => error.ToStatusCode() switch
    {
        StatusCodes.Status404NotFound => "Not Found",
        StatusCodes.Status403Forbidden => "Forbidden",
        StatusCodes.Status401Unauthorized => "Unauthorized",
        StatusCodes.Status409Conflict => "Conflict",
        _ => "Bad Request"
    };

    public static ProblemDetails ToProblemDetails(this IError error)
    {
        var status = error.ToStatusCode();

        var problem = new ProblemDetails
        {
            Status = status,
            Title = error.ToTitle(),
            Detail = error.Message
        };

        var field = error.GetField();

        if (field is not null)
            problem.Extensions["field"] = field;

        if (error is TooShortError tooShort)
            problem.Extensions["minCharsLength"] = tooShort.MinCharsLength;

        if (error is TooLongError tooLong)
            problem.Extensions["maxCharsLength"] = tooLong.MaxCharsLength;

        return problem;
    }

    public static string? GetField(this IError error) => error is FieldError fieldError ? fieldError.Field : null;
}
