using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace web_api.Middleware
{
    public class JWTMiddleware
    {
        //private readonly RequestDelegate _next;

        //public JWTMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task InvokeAsync(HttpContext context)
        //{
           
        //    var endpoint = context.GetEndpoint();
        //    if (endpoint?.Metadata.GetMetadata<JWTMiddlewareAttribute>() is JWTMiddlewareAttribute attribute &&
        //        attribute.MetadataProperty == "metadata-value")
        //    {
        //        // Your custom authentication logic here

        //        if (!context.Request.Headers.ContainsKey("Authorization"))
        //        {
        //            context.Response.StatusCode = 401;
        //            await context.Response.WriteAsync("Unauthorized");
        //            return;
        //        }

               
        //    }



        //}
    }
}
