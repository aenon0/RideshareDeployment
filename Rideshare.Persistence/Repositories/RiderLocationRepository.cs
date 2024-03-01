using MongoDB.Bson;
using MongoDB.Driver;
using Rideshare.Domain.Entities;
using Rideshare.Persistence.Repositories;

namespace Rideshare_backend;

public class RiderLocationRepository : GenericRepository<RiderLocation>, IRiderLocationRepository
{
    private readonly  IMongoCollection<RiderLocation> _riderLocationCollection;
    public RiderLocationRepository(IMongoDatabase database) : base(database)
    {
        _riderLocationCollection = database.GetCollection<RiderLocation>("RiderLocation");
    }

    public async Task<IReadOnlyList<RiderLocation>> GetByRiderId(ObjectId riderId)
    {
        var filter = Builders<RiderLocation>.Filter.Eq("RiderId", riderId);
        var riderLocations = await _riderLocationCollection.Find(filter).ToListAsync();
        return riderLocations;
    }
}
