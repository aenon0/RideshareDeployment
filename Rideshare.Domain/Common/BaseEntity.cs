using MongoDB.Bson.Serialization.Attributes;

namespace Rideshare.Domain.Common;

public class BaseEntity
{
    [BsonElement("custom_id")]
    public Guid Id {set; get;}
    public DateTime createdAt {set; get;}
}
