
using SchoolHubApi.Repositories;

namespace SchoolHubApi.Extensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITableRepository, TableRepository>();

        return services;
    }
}
