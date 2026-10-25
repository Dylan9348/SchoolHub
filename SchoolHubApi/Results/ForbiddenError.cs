
using FluentResults;

namespace SchoolHubApi.Results;

public class ForbiddenError(string field, string message) : FieldError(field, message);
