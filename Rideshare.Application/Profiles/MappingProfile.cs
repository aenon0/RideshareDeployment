using AutoMapper;
using Rideshare.Application.Features.Package.Dtos;
using Rideshare.Application.Features.RiderLocation.Dtos;
using Rideshare.Domain.Entities;

namespace Rideshare.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
             CreateMap<Package, GetMatchingPackageResponseDto>().ReverseMap();
             CreateMap<Package, GetPackageResponseDto>().ReverseMap();

             CreateMap<RiderHistory, GetPackagesByRiderIdResponseDto>().ReverseMap();

             CreateMap<RiderLocation, GetRiderLocationsResponseDto>().ReverseMap();



        }


    }
}
