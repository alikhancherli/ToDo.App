using System.Security.Claims;

namespace ToDo.App.Infrastructure.Extensions
{
    public static class JwtExtensions
    {
        public static IList<KeyValuePair<string, string>> ToIdentityClaims(this ClaimsPrincipal principal)
        {
            var claims = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("sid",principal.GetNullableValue(ClaimTypes.Sid)),
                new KeyValuePair<string, string>("username",principal.GetNullableValue(ClaimTypes.Name)),
                new KeyValuePair<string, string>("phoneNumber",principal.GetNullableValue(ClaimTypes.MobilePhone)),
                new KeyValuePair<string, string>("email",principal.GetNullableValue(ClaimTypes.Email))
            };

            return claims;
        }

        private static string? GetNullableValue(this ClaimsPrincipal principal, string type)
        {
            return principal.FindFirst(type)?.Value;
        }
    }
}
