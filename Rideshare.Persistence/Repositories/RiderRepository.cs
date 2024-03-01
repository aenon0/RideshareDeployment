using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Rideshare.Domain.Entities;
using Rideshare_backend;

namespace Rideshare.Persistence.Repositories;

public class RiderRepository : GenericRepository<Rider> ,IRiderRepository
{
    private readonly IMongoCollection<Rider>? _riderCollection;
    
    public RiderRepository(IMongoDatabase database) : base(database)
    {
        _riderCollection = database.GetCollection<Rider>("Rider");
    }

    public async Task<bool> ExistsByPhoneNumber(string phoneNumber)
    {
        var result = await _riderCollection.Find(r => r.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        return result != null;
    }

    public async Task<Rider> GetByPhoneNUmber(string phoneNumber)
    {
        return await _riderCollection.Find(r => r.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
    }
}

