using System.Reflection;
using MongoDB.Driver;
using Rideshare.Application.Contracts.Persistence;

namespace Rideshare_backend
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Delete(T entity)
        {
           PropertyInfo idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                object idValue = idProperty.GetValue(entity);
                await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", idValue));
            }
            else
            {
                throw new InvalidOperationException("Entity does not have an 'Id' property.");
            }
        }

        public async Task<bool> Exists(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var count = await _collection.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<T?> Get(int id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task Update(T entity)
        {
            PropertyInfo idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                object idValue = idProperty.GetValue(entity);
                await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", idValue), entity);
            }
            else
            {
                throw new InvalidOperationException("Entity does not have an 'Id' property.");
            }
                }
            }
}
