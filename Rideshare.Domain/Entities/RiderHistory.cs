using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Rideshare.Domain.Common;

namespace Rideshare.Domain.Entities;

public class RiderHistory: BaseEntity
{
    public ObjectId PackageId {set; get;}
    public ObjectId RiderId {set; get;}
    public string Name {set; get;}
    public Location PickUpLocation {set; get;}
    public Location DropOffLocation {set; get;}
    public bool IsValid {set; get;} = true;
    // public int RatingId {set; get;}
    // public string PaymentType {set; get;}
    // public string TransactionId {set; get;}
    // public double Distance {set; get;}

}
