using MongoDB.Bson;

namespace Rideshare.Application.Features.Package.Dtos;

public interface IPackageDto
{
    ObjectId Id { set; get; }
}
