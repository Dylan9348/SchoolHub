
using SchoolHubApi.Handlers;

namespace SchoolHubApi.Extensions;

public static class ExceptionHandlersExtension
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
