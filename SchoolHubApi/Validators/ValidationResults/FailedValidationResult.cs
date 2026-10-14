
using SchoolHubApi.Enums;

namespace SchoolHubApi.Validators.ValidationResults;

public class FailedValidationResult(FailedValidationType type, object? wrongValue = null, string? message = null) : ValidationResult
{
    public object? WrongValue { get; } = wrongValue;
    public string? Message { get; } = message;
    public FailedValidationType Type { get; } = type;
}
