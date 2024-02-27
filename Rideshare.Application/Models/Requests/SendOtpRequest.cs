using System.ComponentModel.DataAnnotations;

namespace Rideshare.Application.Models.Requests;

public class SendOtpRequest
{
    [Required]
    public string PhoneNumber {get; set;}
}
