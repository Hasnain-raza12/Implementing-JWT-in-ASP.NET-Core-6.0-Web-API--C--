using Api.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Registeration : ControllerBase
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("practice");
        static IMongoCollection<Login> collection = db.GetCollection<Login>("login");
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Register([FromBody] Login user)
        {
            bool exists = collection.Find(_ => _.email == user.email).Any();
            if (exists)
            {

                return BadRequest("Email Alredy Exist");
            }
            else
            {
                Login s = new Login(user.email, user.password);
                collection.InsertOne(s);
                return Ok("Account Created" + s);
            }
        }
    }
}
