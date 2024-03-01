using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.RiderLocation.Dtos;

namespace Rideshare.Application.Features.RiderLocation.Requests.Handlers;
public class GetRiderLocationsByRiderIdRequest : IRequest<BaseResponse<List<GetRiderLocationsResponseDto>>>
{
    public GetRiderLocationsRequestDto GetRiderLocationsRequestDto {set; get;}
}