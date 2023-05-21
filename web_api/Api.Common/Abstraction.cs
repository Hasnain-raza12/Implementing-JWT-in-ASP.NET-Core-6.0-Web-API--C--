using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Common
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string? Id { get; set; }
    }
    public interface IRepositry<T> where T: IEntity { 
    
         void Create  (T entity);
        void Update (T entity);
        
        void Delete (string id);

        T GetbyId (string id);
        IReadOnlyCollection<T> GetAll ();
    }
}