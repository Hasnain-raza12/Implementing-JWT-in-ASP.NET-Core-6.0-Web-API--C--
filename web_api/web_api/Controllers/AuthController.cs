using Amazon.SecurityToken.Model;
using Api.Common.Models;
using DnsClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("practice");
        static IMongoCollection<Login> collection = db.GetCollection<Login>("login");
        IConfiguration configuration;
        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        //[AllowAnonymous]
        //[HttpPost]
        // [HttpPost("login")]
        //public IActionResult Auth([FromBody] User user)
        //{
        //    IActionResult response = Unauthorized();
        //    if (user != null)
        //    {
        //        if (user.UserName.Equals("test@email.com") && user.Password.Equals("a"))
        //        {

        //            var issuer = configuration["Jwt:Issuer"];
        //            var audience = configuration["Jwt:Audience"];
        //            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        //            var signingCredentials = new SigningCredentials(
        //                                    new SymmetricSecurityKey(key),
        //                                    SecurityAlgorithms.HmacSha512Signature
        //             );

        //            var subject = new ClaimsIdentity(new[]
        //            {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Email, user.UserName),
        //            });
        //            var expires = DateTime.UtcNow.AddMinutes(10);
        //            var tokenDescriptor = new SecurityTokenDescriptor
        //            {
        //                Subject = subject,
        //                Expires = DateTime.UtcNow.AddMinutes(10),
        //                Issuer = issuer,
        //                Audience = audience,
        //                SigningCredentials = signingCredentials
        //            };
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var token = tokenHandler.CreateToken(tokenDescriptor);
        //            var jwtToken = tokenHandler.WriteToken(token);
        //            return Ok(jwtToken);
        //        }
        //    }
        //    return response;
        //}

        [AllowAnonymous]
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Auth([FromBody] Login user)
        {
            IActionResult response = Unauthorized();
            // Process the authentication request
            bool isAuthenticated = AuthenticateUser(user.email, user.password);

            if (isAuthenticated)
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(
                                        new SymmetricSecurityKey(key),
                                        SecurityAlgorithms.HmacSha512Signature
                 );

                var subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.email),
                    new Claim(JwtRegisteredClaimNames.Email, user.email),
                    });
                var expires = DateTime.UtcNow.AddMinutes(10);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest("Authentication failed");
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            var filter = Builders<Login>.Filter.Eq("email", username) & Builders<Login>.Filter.Eq("password", password);
            var result = collection.Find(filter).FirstOrDefault();
            return result != null;

        }
    }
}
