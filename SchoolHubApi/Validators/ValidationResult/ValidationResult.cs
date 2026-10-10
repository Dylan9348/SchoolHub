
namespace SchoolHubApi.Models.ValidationResultModels;

public abstract class ValidationResult<T>(T validatedObject)
{
    public T ValidatedObject { get; } = validatedObject;
}
