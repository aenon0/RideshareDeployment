using System.Runtime.CompilerServices;
using MediatR;
using Rideshare.Application.Contracts.Infrastructure;
using Rideshare.Application.Features.Auth.CQRS.Commands;
using Rideshare.Application.Models.Requests;
using Rideshare.Application.Models.Responses;
using Rideshare.Application.Services;
using Rideshare.Domain.Entities;
using Rideshare_backend;

namespace Rideshare.Application.Features.Auth.CQRS.Handlers;

public class VerificationCommandHandler : IRequestHandler<VerificationCommand, VerifyOtpResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IRiderRepository _riderRepository;
    private readonly IOtpRepository _otpRepository;
    public VerificationCommandHandler(IRiderRepository riderRepository, IJwtGenerator jwtGenerator, IOtpRepository otpRepository)
    {
        _jwtGenerator = jwtGenerator;
        _riderRepository = riderRepository;
        _otpRepository = otpRepository;
    }

    public async Task<VerifyOtpResponse> Handle(VerificationCommand request, CancellationToken cancellationToken)
    {
        var response = new VerifyOtpResponse { Id = "", Token = "" };
        // if the otp if right then send a token and rider's Id else send empty values
        if (request.verifyOtpRequest.OtpCode == (await _otpRepository.GetByPhoneNumber(request.verifyOtpRequest.PhoneNumber)).OtpCode)
        {
            Rider rider = await _riderRepository.GetByPhoneNUmber(request.verifyOtpRequest.PhoneNumber);
            var token = _jwtGenerator.Generate(rider);
            // Console.WriteLine(token);
            response.Token = token;
            response.Id = rider.Id.ToString();
        }
        // Console.WriteLine(request.verifyOtpRequest.OtpCode);
        // var otp = await _otpRepository.GetByPhoneNumber(request.verifyOtpRequest.PhoneNumber);
        // Console.WriteLine(otp.OtpCode);
        return response;
    }
}