using MongoDB.Driver;
using Rideshare.Application.Contracts.Persistence;
using Rideshare.Domain.Common;
using Rideshare.Domain.Entities;

namespace Rideshare.Persistence.Repositories;

public class PackageRepository : GenericRepository<Package>, IPackageRepository
{
    private readonly  IMongoCollection<Package> _packageCollection;
    public PackageRepository(IMongoDatabase database): base(database)
    {
        _packageCollection = database.GetCollection<Package>("Package");
    }
    public async Task<IReadOnlyList<Package>> GetPackagesByPreference(Location pickUpLocation, Location dropOffLocation, DateTime startDateTime, VehicleType vehicleType, PackageType packageType)
    {
        var filterBuilder = Builders<Package>.Filter;
        var filter = filterBuilder.Eq(x => x.PickUpLocation.Latitude, pickUpLocation.Latitude) &
                    filterBuilder.Eq(x => x.PickUpLocation.Longitude, pickUpLocation.Longitude) &
                    filterBuilder.Eq(x => x.DropOffLocation.Latitude, dropOffLocation.Latitude) &
                    filterBuilder.Eq(x => x.DropOffLocation.Longitude, dropOffLocation.Longitude) &
                    filterBuilder.Gte(x => x.StartDateTime, startDateTime.AddMilliseconds(-1)) &
                    filterBuilder.Eq(x => x.VehicleType, vehicleType) &
                    filterBuilder.Eq(x => x.PackageType, packageType);

        var packages = await _packageCollection.Find(filter).ToListAsync();
        return packages;
    }
}
