
using SchoolHubApi.Repositories;

namespace SchoolHubApi.Extensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IWorkRepository, WorkRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        return services;
    }
}
