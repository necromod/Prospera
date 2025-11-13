using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Prospera.Helpers
{
    public static class HttpContextExtensions
    {
        public static int? GetUserId(this HttpContext httpContext)
        {
            if (httpContext?.User == null) return null;
            var idClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(idClaim, out var id)) return id;
            return null;
        }

        public static string? GetUserName(this HttpContext httpContext)
        {
            return httpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
