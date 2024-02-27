using Rideshare.Application.Contracts.Persistence;
using Rideshare.Domain;
using Rideshare.Domain.Entities;

namespace Rideshare.Application.Contracts.Persistence;

public interface IRiderHistoryRepository : IGenericRepository<RiderHistory>
{
    Task<IReadOnlyList<RiderHistory>> GetByUserId(string packageId);
}
