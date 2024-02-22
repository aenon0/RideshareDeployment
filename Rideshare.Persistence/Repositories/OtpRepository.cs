using MongoDB.Driver;
using Rideshare.Domain.Entities;

namespace Rideshare_backend;

public class OtpRepository : GenericRepository<Otp>, IOtpRepository
{
    private readonly IMongoCollection<Otp> _otpCollection;

     public OtpRepository(IMongoDatabase database) : base(database)
        {
            _otpCollection = database.GetCollection<Otp>("Otp");
        }

    public async Task<Otp> GetByPhoneNumber(string phoneNumber)
    {
        var filter = Builders<Otp>.Filter.Eq(e => e.PhoneNumber, phoneNumber);
        var otp = await _otpCollection.Find(filter).FirstOrDefaultAsync();
        return otp;
    }

}
