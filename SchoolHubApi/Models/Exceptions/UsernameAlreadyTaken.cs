namespace SchoolHubApi.Models.Exceptions;

public class UsernameAlreadyTakenException(string username)
    : ConflicException($"Username \"{username}\" already taken");
