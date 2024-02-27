using System.Reflection;
using MongoDB.Bson;
using MongoDB.Driver;
using Rideshare.Application.Contracts.Persistence;

namespace Rideshare.Persistence.Repositories;

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

    public async Task<bool> Exists(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var count = await _collection.CountDocumentsAsync(filter);
        return count > 0;
    }

    public async Task<T?> Get(string id)
    {
        var convertedId = ObjectId.Parse(id); // Convert the id to the appropriate type
        var filter = Builders<T>.Filter.Eq("_id", convertedId);
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

