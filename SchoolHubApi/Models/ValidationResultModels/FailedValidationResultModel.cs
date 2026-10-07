
namespace SchoolHubApi.Models.ValidationResultModels;

public class FailedValidationResult<VT, WT>(VT validatedObject, WT wrongValue, string? message = null) : ValidationResult<VT>(validatedObject)
{
    public WT WrongValue { get; } = wrongValue;
    public string? Message { get; set; } = message;
}