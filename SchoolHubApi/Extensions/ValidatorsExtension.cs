
using SchoolHubApi.Validators;

namespace SchoolHubApi.Extensions;

public static class ValidatorsException
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<PendingRegistrationValidator>();

        return services;
    }
}
