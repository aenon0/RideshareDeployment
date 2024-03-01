using AutoMapper;
using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.RiderLocation.Dtos;
using Rideshare.Application.Features.RiderLocation.Requests.Handlers;
using Rideshare_backend;

namespace Rideshare.Application.Features.RiderLocation.CQRS.Handlers
{
    public class GetRiderLocationsByRiderIdRequestHandler : IRequestHandler<GetRiderLocationsByRiderIdRequest, BaseResponse<List<GetRiderLocationsResponseDto>>>
    {
        private readonly IRiderLocationRepository _riderLocationepository;
        private readonly IMapper _mapper;

        public GetRiderLocationsByRiderIdRequestHandler(IRiderLocationRepository riderLocationRepository, IMapper mapper)
        {
            _riderLocationepository = riderLocationRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<List<GetRiderLocationsResponseDto>>> Handle(GetRiderLocationsByRiderIdRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<List<GetRiderLocationsResponseDto>>();
            var riderLocations = await _riderLocationepository.GetByRiderId(request.GetRiderLocationsRequestDto.RiderId);
            var riderLocationDtos = _mapper.Map<List<GetRiderLocationsResponseDto>>(riderLocations);
            response.Value = riderLocationDtos;
            response.IsSuccess = true;
            return response;
        }
    }
}
