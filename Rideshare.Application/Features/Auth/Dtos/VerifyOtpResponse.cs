using MongoDB.Bson;

namespace Rideshare.Application.Features.Auth.Dtos;

public class VerifyOtpResponse
{
    public ObjectId Id { get; set; }
    public string Token { get; set; }
}
