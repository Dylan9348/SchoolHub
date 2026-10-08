
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SchoolHubApi.Extensions;

public static class JwtExtension
{
    public static IServiceCollection AddJwt(this IServiceCollection services)
    {
        static string GetEnvVar(string varName) =>
            Environment.GetEnvironmentVariable(varName)
                ?? throw new InvalidOperationException($"Environment variable {varName} not found");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = GetEnvVar("JWT_ISSUER"),

                    ValidateAudience = true,
                    ValidAudience = GetEnvVar("JWT_AUDIENCE"),

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetEnvVar("JWT_KEY")))
                };
            });

        return services;
    }
}
