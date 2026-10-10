namespace SchoolHubApi.Models.Exceptions;

public class NotFoundException(string message) : SchoolHubException(message);
