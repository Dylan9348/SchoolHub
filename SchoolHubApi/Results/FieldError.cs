
using FluentResults;

namespace SchoolHubApi.Results;

public abstract class FieldError(string field, string message) : Error(message)
{
    public string Field { get; } = field;
}
