using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Presentation.HangFire
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            HttpContext httpContext = context.GetHttpContext();
            if (httpContext == null) return false;
            string userRole = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == null) return false;
            return userRole.Equals("Admin");
        }
    }
}
