using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Api.Common
{
    public class Repositry<T> : IRepositry<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _dbcollections;
        private readonly FilterDefinitionBuilder<T> filter = Builders<T>.Filter;
        public Repositry(IMongoDatabase database,string collectionName) 
        {
            _dbcollections = database.GetCollection<T>(collectionName);
        }
        public void Create(T entity)
        {
            if (entity!=null)
            {
                _dbcollections.InsertOne(entity);
            }
        }

        public void Delete(string id)
        {
             _dbcollections.DeleteOne(filter.Eq(t => t.Id, id));
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _dbcollections.Find(filter.Empty).ToList();
        }

        public T GetbyId(string id)
        {
            return _dbcollections.Find(filter.Eq(t=>t.Id, id)).FirstOrDefault();
        }

        public void Update(T entity)
        {
            _dbcollections.ReplaceOne(filter.Eq(t => t.Id, entity.Id),entity);
        }
    }
}
