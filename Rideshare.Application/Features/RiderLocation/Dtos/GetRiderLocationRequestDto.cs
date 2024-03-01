using MongoDB.Bson;

namespace Rideshare.Application.Features.RiderLocation.Dtos;

public class GetRiderLocationsRequestDto
{
    public ObjectId RiderId {set; get;}
}
