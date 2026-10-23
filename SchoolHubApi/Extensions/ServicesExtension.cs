
using SchoolHubApi.Services.Auth;
using SchoolHubApi.Services.General;
using SchoolHubApi.Services.Tables;

namespace SchoolHubApi.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailAuthService, EmailAuthService>();
        services.AddScoped<ITokenAuthService, TokenAuthService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IWorkService, WorkService>();

        return services;
    }
}
