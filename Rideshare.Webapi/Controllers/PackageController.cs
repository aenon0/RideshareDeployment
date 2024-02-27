using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.Package.CQRS.Commands;
using Rideshare.Application.Features.Package.Dtos;

namespace Rideshare.WebApi.Controllers;

[ApiController]
[Route("api/packages")]
public class PackageController : ControllerBase
{
    private readonly IMediator _mediator;

    public PackageController(IMediator mediator)
    {
        _mediator = mediator;
    }

        [HttpPost("GetMatchingPackages")]
        public async Task<ActionResult<BaseResponse<List<GetMatchingPackageResponseDto>>>> GetMatchingPackages([FromBody] GetMatchingPackageRequestDto request)
        {
            var response = await _mediator.Send(new GetMatchingPackageRequest { GetMatchingPackageRequestDto = request });
            return Ok(response);
        }

        [HttpGet("GetPackage/{packageId}")]
        public async Task<ActionResult<BaseResponse<GetPackageResponseDto>>> GetPackage(string packageId)
        {
            var response = await _mediator.Send(new GetPackageRequest { GetPackageRequestDto = new GetPackageRequestDto { Id =  packageId} });
            return Ok(response);
        }
    }
