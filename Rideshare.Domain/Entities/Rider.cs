
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Rideshare.Domain.Common;

namespace Rideshare.Domain.Entities
{
    public class Rider : BaseEntity
    {
        public string PhoneNumber { set; get; }
    }
}
