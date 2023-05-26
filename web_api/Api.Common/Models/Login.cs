using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Common.Models
{
    public class Login
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement]
        public string email { get; set; }

        [BsonElement]
        public string password { get; set; }

        public Login(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
