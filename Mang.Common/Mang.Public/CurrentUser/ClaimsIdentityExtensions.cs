using System.Linq;
using System.Security.Claims;
using Mang.Public.Extension;

namespace Mang.Public.CurrentUser
{
    public static class ClaimsIdentityExtensions
    {
        public static int FindUserId(this ClaimsPrincipal principal)
        {
            Claim userIdOrNull = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
            {
                return 0;
            }

            return int.Parse(userIdOrNull.Value);
        }

        public static bool? IsAdminUser(this ClaimsPrincipal principal)
        {
            Claim isAdminOrNull = principal.Claims.FirstOrDefault(c => c.Type == GameClaimTypes.IsAdmin);
            if (isAdminOrNull == null || isAdminOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            return bool.Parse(isAdminOrNull.Value);
        }

        public static bool? IsSuperAdmin(this ClaimsPrincipal principal)
        {
            Claim isAdminOrNull = principal.Claims.FirstOrDefault(c => c.Type == GameClaimTypes.IsSuperAdmin);
            if (isAdminOrNull == null || isAdminOrNull.Value.IsNullOrWhiteSpace())
            {
                return null;
            }

            return bool.Parse(isAdminOrNull.Value);
        }

        public static string FindUserName(this ClaimsPrincipal principal)
        {
            Claim userNameOrNull = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return userNameOrNull?.Value;
        }

        public static string FindPhone(this ClaimsPrincipal principal)
        {
            Claim phoneOrNull = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone);

            return phoneOrNull?.Value;
        }

        public static string FindAvatar(this ClaimsPrincipal principal)
        {
            Claim userNameOrNull = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Actor);

            return userNameOrNull?.Value;
        }
    }
}