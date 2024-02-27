using MongoDB.Driver;
using Rideshare.Domain.Entities;

namespace Rideshare.Persistence.Repositories;

public class OtpRepository : GenericRepository<Otp>, IOtpRepository
{
    private readonly IMongoCollection<Otp> _otpCollection;

    public OtpRepository(IMongoDatabase database) : base(database)
    {
        _otpCollection = database.GetCollection<Otp>("Otp");
    }

    public Task<Otp> GetByPhoneNumber(string phoneNumber)
    {
        throw new NotImplementedException();
    }

}
