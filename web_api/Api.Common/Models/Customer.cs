using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Common.Models
{
    public class Customer: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]


        public string? Id { get; set; }
      
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
