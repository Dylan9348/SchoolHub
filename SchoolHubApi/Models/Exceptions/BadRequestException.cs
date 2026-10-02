
namespace SchoolHubApi.Models.Exceptions;

public class BadRequestException(string entityName, object value, string? reason = null) : SchoolHubException($"Invalid {entityName}: {value}. {reason}");
