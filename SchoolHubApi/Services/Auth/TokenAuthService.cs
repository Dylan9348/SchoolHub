using System.Security.Cryptography;

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
}
