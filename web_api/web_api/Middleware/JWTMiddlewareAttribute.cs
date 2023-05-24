using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace web_api.Middleware
{
   
    public class JWTMiddlewareAttribute: AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the request is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // Return 401 Unauthorized if not authenticated
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
