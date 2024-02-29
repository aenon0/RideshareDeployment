using MongoDB.Bson;

namespace Rideshare.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : class
{
    Task<T?> Get(ObjectId id);
    Task<List<T>> GetAll();
    Task Add(T entity);
    Task<bool> Exists(ObjectId id);
    Task Update(T entity);
    Task Delete(T entity);
}