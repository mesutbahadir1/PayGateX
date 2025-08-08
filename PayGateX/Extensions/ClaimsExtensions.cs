using System.Security.Claims;

namespace PayGateX.Extensions;

public static class ClaimsExtensions
{
    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user.Claims
            .SingleOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
    }

    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
    }

}