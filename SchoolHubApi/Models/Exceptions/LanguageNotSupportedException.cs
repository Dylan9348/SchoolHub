
namespace SchoolHubApi.Models.Exceptions;

public class LanguageNotSupportedException(string language) : BadRequestException("language", language);