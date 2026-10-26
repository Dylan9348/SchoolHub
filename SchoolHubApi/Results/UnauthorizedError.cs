
using FluentResults;

namespace SchoolHubApi.Results;

public class UnauthorizedError(string field, string message) : FieldError(field, message);
