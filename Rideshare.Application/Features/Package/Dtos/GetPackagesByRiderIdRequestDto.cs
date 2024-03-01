using MongoDB.Bson;

namespace Rideshare.Application.Features.Package.Dtos;

public class GetPackagesByRiderIdRequestDto
{
   public ObjectId RiderId {set; get;}
}
