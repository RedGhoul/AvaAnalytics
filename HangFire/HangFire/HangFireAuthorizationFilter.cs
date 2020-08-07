using Hangfire.Dashboard;
using System.Security.Claims;

namespace HangFire
{
    public class HangFireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            Microsoft.AspNetCore.Http.HttpContext httpContext = context.GetHttpContext();
            string useRole = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            return true;//useRole == "Admin";
        }
    }
}
