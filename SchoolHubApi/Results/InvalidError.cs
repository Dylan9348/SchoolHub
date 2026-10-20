
using FluentResults;

namespace SchoolHubApi.Results;

public class InvalidError(string field, string message) : FieldError(field, message);
