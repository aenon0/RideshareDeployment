using Rideshare.Domain.Entities;

namespace Rideshare.Application.Contracts.Infrastructure;

public interface IJwtGenerator
{
    public string Generate(Rider rider);
}