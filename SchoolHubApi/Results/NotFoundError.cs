
using FluentResults;

namespace SchoolHubApi.Results;

public class NotFoundError(string field, string message) : FieldError(field, message);
