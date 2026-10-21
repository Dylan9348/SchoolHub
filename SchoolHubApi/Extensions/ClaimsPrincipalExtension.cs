
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolHubApi.Extensions;

public static class ClaimsPrincipalExtension
{
    public static Guid? GetUserId(this ClaimsPrincipal principal)
    {
        var userStringId = principal.FindFirstValue(ClaimTypes.NameIdentifier)
                           ?? principal.FindFirstValue(JwtRegisteredClaimNames.Sub);

        return Guid.TryParse(userStringId, out var userId) ? userId : null;
    }
}
