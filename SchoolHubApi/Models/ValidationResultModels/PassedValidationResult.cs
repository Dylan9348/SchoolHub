
namespace SchoolHubApi.Models.ValidationResultModels;

public class PassedValidationResult<T>(T validatedObject) : ValidationResult<T>(validatedObject);
