using MongoDB.Bson;
using Rideshare.Application.Contracts.Persistence;
using Rideshare.Domain.Entities;

namespace Rideshare_backend;

public interface IRiderLocationRepository : IGenericRepository<RiderLocation>
{
     Task<IReadOnlyList<RiderLocation>> GetByRiderId(ObjectId riderId);
}
