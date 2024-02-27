using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rideshare.Domain.Common;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {set; get;}
    public DateTime createdAt {set; get;} = DateTime.UtcNow;
}
