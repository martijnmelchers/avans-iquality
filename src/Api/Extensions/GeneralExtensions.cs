using System.Linq;
using System.Security.Claims;

namespace IQuality.Api.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}