
using FluentResults;

namespace SchoolHubApi.Results;

public class ConflictError(string field, string message) : FieldError(field, message);
