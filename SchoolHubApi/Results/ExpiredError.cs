
using FluentResults;

namespace SchoolHubApi.Results;

public class ExpiredError(string message) : Error(message);
