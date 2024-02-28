
using MongoDB.Bson;

namespace Rideshare.Application.Features.Package.Dtos;

public class GetPackageRequestDto : IPackageDto
{
    public ObjectId Id { set; get;}
}
