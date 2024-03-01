using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rideshare.Application.Features.Auth.CQRS.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MongoDB.Bson;
using Rideshare.Application.Features.Auth.Dtos;
using Rideshare.Application.Common.Response;

namespace Rideshare.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("SendOtp")]
    public async Task<ActionResult<BaseCommandResponse>> Register([FromBody] SendOtpRequest request)
    {
        Console.WriteLine(request);
        var response = await _mediator.Send(new SendOtpCommand { sendOtpRequest = request });
        if(response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    [HttpPost("VerifyOtp")]
    public async Task<ActionResult<VerifyOtpResponse>> Login([FromBody] VerifyOtpRequest request)
    {
        var response = await _mediator.Send(new VerificationCommand { verifyOtpRequest = request });
        if(response.IsSuccess)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}