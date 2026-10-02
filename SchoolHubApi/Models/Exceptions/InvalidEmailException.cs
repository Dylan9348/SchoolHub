
namespace SchoolHubApi.Models.Exceptions;

public class InvalidEmailException(string email, string? reason = null) : BadRequestException("email", email, reason);
