using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace web_api.Middleware
{
    public class JWTMiddleware
    {

        private readonly RequestDelegate _next;
        IConfiguration configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero // Disable clock skew
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var expiryDateUnix = long.Parse(jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value);
                var expiryDateUtc = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    .AddSeconds(expiryDateUnix);

                if (expiryDateUtc < DateTime.UtcNow)
                    throw new SecurityTokenExpiredException("Token expired");

                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                context.Items["UserId"] = userId;
            }
            catch (Exception)
            {
                // Token validation failed
                // You can handle the exception or simply do nothing to continue processing the request
            }
        }
    }
}
