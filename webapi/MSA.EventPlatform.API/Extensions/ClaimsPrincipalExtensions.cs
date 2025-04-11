using System.Security.Claims;

namespace MSA.EventPlatform.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token.");

            if (!long.TryParse(userIdClaim.Value, out var userId))
                throw new UnauthorizedAccessException("Invalid User ID format.");

            return userId;
        }
    }
}
