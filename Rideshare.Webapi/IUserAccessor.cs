using MongoDB.Bson;

namespace Rideshare.WebApi;

public interface IUserAccessor
{
    ObjectId GetUserId();
}