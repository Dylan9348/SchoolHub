using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SchoolHubApi.Models.UserModels;

namespace SchoolHubApi.Services.Auth;

public class TokenAuthService : ITokenAuthService
{
    public string GenerateCodeEmailVerification()
    {
        const string permittedChars = "1234567890";

        var code = RandomNumberGenerator.GetString(permittedChars, 7);

        return code;
    }

    public string GenerateSecureRandomString(int length = 10)
    {

        const string permittedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        var randomString = RandomNumberGenerator.GetString(permittedChars, length);

        return randomString;
    }

    public string GenerateJwt(Guid userId, UserRole role, string email)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(ClaimTypes.Role, role.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        static string GetEnvVar(string varName) =>
            Environment.GetEnvironmentVariable(varName)
                ?? throw new InvalidOperationException($"Environment variable: {varName} not found.");

        var envKey = GetEnvVar("JWT_KEY");
        var issuer = GetEnvVar("JWT_ISSUER");
        var audience = GetEnvVar("JWT_AUDIENCE");
        var expiration = int.Parse(GetEnvVar("JWT_EXPIRATION_MINUTES"));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(expiration),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
