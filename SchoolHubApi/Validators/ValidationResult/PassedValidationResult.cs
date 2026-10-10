
namespace SchoolHubApi.Validators.ValidationResult;

public class PassedValidationResult<T>(T validatedObject) : ValidationResult<T>(validatedObject);
