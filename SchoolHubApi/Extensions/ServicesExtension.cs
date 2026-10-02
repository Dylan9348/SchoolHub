
using SchoolHubApi.Services.Auth;

namespace SchoolHubApi.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailAuthService, EmailAuthService>();
        services.AddScoped<ITokenAuthService, TokenAuthService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();

        return services;
    }
}
