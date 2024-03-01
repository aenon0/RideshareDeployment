using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.RiderLocation.Dtos;
using Rideshare.Application.Features.RiderLocation.Requests.Handlers;
namespace Rideshare.WebApi.Controllers;

[ApiController]
[Route("api/locations")]
[Authorize(Policy = "RiderPolicy")]
public class RiderLocationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserAccessor _userAccessor; 

    public RiderLocationController(IMediator mediator, IUserAccessor userAccessor)
    {
        _mediator = mediator;
        _userAccessor = userAccessor;
    }


    [HttpGet("GetRiderLocations")]
    public async Task<ActionResult<BaseResponse<List<GetRiderLocationsResponseDto>>>> GetSavedLocations()
    {
        var riderId = _userAccessor.GetUserId();
        var response = await _mediator.Send(new GetRiderLocationsByRiderIdRequest { GetRiderLocationsRequestDto = new GetRiderLocationsRequestDto { RiderId =  riderId} });
        if(response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("SaveRiderLocation")]
    public async Task<ActionResult<BaseResponse<List<GetRiderLocationsResponseDto>>>> SaveLocation([FromBody] CreateRiderLocationRequestDto request)
    {
        var riderId = _userAccessor.GetUserId();
        request.RiderId = riderId;
        var response = await _mediator.Send(new CreateRiderLocationRequest { CreateRiderLocationRequestDto = request});
        if(response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    


}

