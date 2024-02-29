using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Rideshare.WebApi;

namespace Rideshare.WebApi;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ObjectId GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId != null)
        {
            return ObjectId.Parse(userId);
        }
        return ObjectId.Empty;
    }
}