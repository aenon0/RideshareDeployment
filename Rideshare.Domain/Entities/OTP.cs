using Rideshare.Domain.Common;

namespace Rideshare.Domain.Entities;
public class Otp : BaseEntity
{
    public string PhoneNumber { get; set; }
    public string OtpCode { get; set; }
}