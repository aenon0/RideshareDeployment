using Rideshare.Domain.Common;

namespace Rideshare.Domain.Entities;

public class Payment
{
    public Guid Id {set; get;}
    public Guid UserId {set; get;}
    public Guid DriverId {set; get;}
    PaymentMethod PaymentMethod {set; get;}

    public double price {set; get;}
    public string TransactionId {set; get;}
    

}
