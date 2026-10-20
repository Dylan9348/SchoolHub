
using FluentResults;

namespace SchoolHubApi.Results;

public class TooShortError(string field, int minCharsLength, string message) : FieldError(field, message)
{
    public int MinCharsLength { get; } = minCharsLength;
}
