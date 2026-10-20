
using FluentResults;

namespace SchoolHubApi.Results;

public class NoAttemptsLeftError(string message) : Error(message);
