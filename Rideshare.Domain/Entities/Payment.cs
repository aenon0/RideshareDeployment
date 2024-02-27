using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Rideshare.Domain.Common;

namespace Rideshare.Domain.Entities;

public class Payment : BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId {set; get;}
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string DriverId {set; get;}
    PaymentMethod PaymentMethod {set; get;}

    public double price {set; get;}
    public string TransactionId {set; get;}
    

}
