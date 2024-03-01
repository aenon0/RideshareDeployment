using Rideshare.Domain.Common;
using Rideshare.Domain.Entities;

namespace Rideshare.Application.Contracts.Persistence;

public interface IPackageRepository : IGenericRepository<Package>
{
    Task<IReadOnlyList<Package>> GetPackagesByPreference(Location pickUpLocation, Location dropOffLocation, DateTime startDateTime, VehicleType vehicleType, PackageType packageType);
}
