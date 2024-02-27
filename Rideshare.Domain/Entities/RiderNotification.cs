using Rideshare.Domain.Common;
namespace Rideshare.Domain.Entities;

public class RiderNotification : BaseEntity
{
    public RiderNotificationType Type {set; get;}
    public string Description {set; get;}
    public DateTime DateTime{set; get;}
}
