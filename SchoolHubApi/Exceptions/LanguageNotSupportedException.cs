
namespace SchoolHubApi.Exceptions;

public class LanguageNotSupportedException(string language) : BadRequestException("language", language);