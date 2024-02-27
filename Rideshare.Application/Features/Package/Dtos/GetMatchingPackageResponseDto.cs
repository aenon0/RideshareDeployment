
using Rideshare.Domain.Common;

namespace Rideshare.Application.Features.Package.Dtos;

public class GetMatchingPackageResponseDto
{
    public String Id {set; get;}
    public string Name {set; get;}
    public double Price{set; get;}
    public PackageType PackageTypeType {set; get;}
    public VehicleType VehicleType {set; get;}
    public Location PickUpLocation {set; get;}
    public Location DropOffLocation {set; get;}
    public DateTime StartDateTime {set; get;}
    public string TotalSeats {set; get;}
    public string AvailableSeats {set; get;}
}
