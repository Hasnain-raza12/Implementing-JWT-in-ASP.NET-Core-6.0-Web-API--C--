using Amazon.Runtime.Internal.Transform;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;

namespace web_api.Middleware
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("UnAuthorized");

                return;
            }
            var header = context.Request.Headers["Authorization"].ToString();
            var encodedCre = header.Substring(6);
            var creds =Encoding.UTF8.GetString(Convert.FromBase64String(encodedCre));
            string[] uidpwd = creds.Split(':');

            var uid = uidpwd[0];
            var upass = uidpwd[1];

            if (uid=="Hasnain" && upass=="pass")
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("UnAuthorized");
                return;
            }


        }

       
    }
    public static class CustomMiddlewareExtension
    {

        public static IApplicationBuilder MyMiddleware(this IApplicationBuilder app)
        {

            return app.UseMiddleware<MyMiddleware>();
        }
    }
}
