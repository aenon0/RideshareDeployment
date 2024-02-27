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
    public Task<IReadOnlyList<RiderHistory>> GetByUserId(string packageId)
    {
        throw new NotImplementedException();
    }
}
