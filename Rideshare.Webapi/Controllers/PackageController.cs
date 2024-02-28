using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.Package.CQRS.Commands;
using Rideshare.Application.Features.Package.Dtos;
using Rideshare.Domain.Common;

namespace Rideshare.WebApi.Controllers;

[ApiController]
[Route("api/packages")]
[Authorize(Policy = "RiderPolicy")]
public class PackageController : ControllerBase
{
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor; 

        public PackageController(IMediator mediator, IUserAccessor userAccessor)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet("GetMatchingPackages")]
        public async Task<ActionResult<BaseResponse<List<GetMatchingPackageResponseDto>>>> GetMatchingPackages(
            [FromQuery] double pickUpLatitude,
            [FromQuery] double pickUpLongitude,
            [FromQuery] double dropOffLatitude,
            [FromQuery] double dropOffLongitude,
            [FromQuery] DateTime startDateTime,
            [FromQuery] int vehicleType,
            [FromQuery] int packageType)
        {
            var request = new GetMatchingPackageRequestDto{
                PickUpLocation = new Location (pickUpLatitude,pickUpLongitude),
                DropOffLocation = new Location (dropOffLatitude,dropOffLongitude),
                StartDateTime = startDateTime, 
                VehicleType = (VehicleType)vehicleType,
                PackageType = (PackageType)packageType
            };
            var response = await _mediator.Send(new GetMatchingPackageRequest { GetMatchingPackageRequestDto = request });
            if(response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetPackage/{packageId}")]
        public async Task<ActionResult<BaseResponse<GetPackageResponseDto>>> GetPackage(string packageId)
        {
            var response = await _mediator.Send(new GetPackageRequest { GetPackageRequestDto = new GetPackageRequestDto { Id =  ObjectId.Parse(packageId)} });
            return Ok(response);
        }

        [HttpGet("GetPackageByUserId")]
        public async Task<ActionResult<BaseResponse<List<GetPackagesByRiderIdResponseDto>>>> GetPackageByUserId()
        {
            var riderId = _userAccessor.GetUserId();
            var response = await _mediator.Send(new GetPackagesByRiderIdRequest { GetPackagesByRiderIdRequestDto = new GetPackagesByRiderIdRequestDto { RiderId =  riderId} });
            if(response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
