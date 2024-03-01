using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.RiderLocation.Dtos;

namespace Rideshare.Application.Features.RiderLocation.Requests.Handlers;

public class CreateRiderLocationRequest : IRequest<BaseCommandResponse>
{
    public CreateRiderLocationRequestDto CreateRiderLocationRequestDto {set; get;}
}
