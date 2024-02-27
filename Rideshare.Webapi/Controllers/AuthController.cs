using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rideshare.Application.Features.Auth.CQRS.Commands;
using Rideshare.Application.Models.Requests;
using Rideshare.Application.Models.Responses;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
    public async Task<ActionResult<SendOtpResponse>> Register([FromBody] SendOtpRequest request)
    {
        Console.WriteLine(request);
        var response = await _mediator.Send(new SendOtpCommand { sendOtpRequest = request });
        return Ok(response);
    }

    [HttpPost("VerifyOtp")]
    public async Task<ActionResult<VerifyOtpResponse>> Login([FromBody] VerifyOtpRequest request)
    {
        var response = await _mediator.Send(new VerificationCommand { verifyOtpRequest = request });
        return Ok(response);
    }
}