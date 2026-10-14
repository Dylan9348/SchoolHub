
namespace SchoolHubApi.Validators.ValidationResults;

public abstract class ValidationResult
{
    public static implicit operator bool(ValidationResult result)
    {
        return result is PassedValidationResult;
    }
}
