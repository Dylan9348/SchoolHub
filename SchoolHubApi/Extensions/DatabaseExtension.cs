using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;

namespace SchoolHubApi.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var host = Environment.GetEnvironmentVariable("NPGSQL_HOST");
        var port = Environment.GetEnvironmentVariable("NPGSQL_PORT");
        var user = Environment.GetEnvironmentVariable("NPGSQL_USER");
        var password = Environment.GetEnvironmentVariable("NPGSQL_PASSWORD");
        var database = Environment.GetEnvironmentVariable("NPGSQL_DATABASE");

        services.AddDbContext<Context>(
            (options) =>
                options.UseNpgsql(
                    $"Host={host};Port={port};Username={user};Password={password};Database={database}"
                )
        );

        return services;
    }
}
