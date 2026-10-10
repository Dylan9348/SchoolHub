
using SchoolHubApi.Enums;

namespace SchoolHubApi.Exceptions;

public class LanguageNotSupportedException(Language language) : BadRequestException("language", language.ToString());