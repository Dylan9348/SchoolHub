
using FluentResults;

namespace SchoolHubApi.Results;

public class AlreadyTakenError(string field, string message) : ConflictError(field, message);
