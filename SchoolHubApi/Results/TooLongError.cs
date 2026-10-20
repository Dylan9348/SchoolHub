
using FluentResults;

namespace SchoolHubApi.Results;

public class TooLongError(string field, int maxCharsLength, string message) : FieldError(field, message)
{
    public int MaxCharsLength { get; } = maxCharsLength;
}
