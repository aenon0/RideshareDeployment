using Rideshare.Application.Contracts.Persistence;
using Rideshare.Domain.Entities;

namespace Rideshare_backend;

public interface IRiderRepository: IGenericRepository<Rider>
{
    public Task<bool> ExistsByPhoneNumber(string phoneNumber);
    public Task<Rider> GetByPhoneNUmber(string phoneNumber);
}
