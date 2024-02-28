using MongoDB.Bson;
using MongoDB.Driver;
using Rideshare.Application.Contracts.Persistence;
using Rideshare.Domain.Entities;

namespace Rideshare.Persistence.Repositories;

public class RiderHistoryRepository : GenericRepository<RiderHistory>, IRiderHistoryRepository
{
    private readonly  IMongoCollection<RiderHistory> _packageCollection;
    public RiderHistoryRepository(IMongoDatabase database): base(database)
    {
        _packageCollection = database.GetCollection<RiderHistory>("RiderHistory");
    }
    public async Task<IReadOnlyList<RiderHistory>> GetByUserId(ObjectId riderId)
    {
        var filter = Builders<RiderHistory>.Filter.Eq(x => x.RiderId, riderId);
        var result = await _packageCollection.Find(filter).ToListAsync();
        return result;
    }
}
