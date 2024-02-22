using Rideshare.Domain.Common;

namespace Rideshare.Domain.Entities;

public class RiderHistory: BaseEntity
{
    public string RiderId {set; get;}
    public string DriverId {set; get;}
    public int RatingId {set; get;}
    public string CarType {set; get;}
    public Location PickUpLocation {set; get;}
    public Location DropOffLocation {set; get;}
    public string PaymentType {set; get;}
    public string TransactionId {set; get;}
    public double Price {set; get;}
    public double Distance {set; get;}
    public DateTime DateTime {set; get;}

}
