
namespace SchoolHubApi.Validators.ValidationResult;

public abstract class ValidationResult<T>(T validatedObject)
{
    public T ValidatedObject { get; } = validatedObject;
}
