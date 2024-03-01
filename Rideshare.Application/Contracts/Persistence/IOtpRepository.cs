using Rideshare.Application.Contracts.Persistence;
using Rideshare.Domain.Entities;
public interface IOtpRepository : IGenericRepository<Otp>
{
     public Task<Otp> GetByPhoneNumber(string phoneNumber);
}
