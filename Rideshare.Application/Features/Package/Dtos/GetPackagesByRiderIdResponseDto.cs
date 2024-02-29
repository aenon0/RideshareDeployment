using MongoDB.Bson;
using Rideshare.Domain.Common;

namespace Rideshare.Application.Features.Package.Dtos;

public class GetPackagesByRiderIdResponseDto : IPackageDto
{
    public ObjectId Id {set; get;}
    public ObjectId RiderId {set; get;}
    public string Name {set; get;}
    public Location PickUpLocation {set; get;}
    public Location DropOffLocation {set; get;}
    
}
