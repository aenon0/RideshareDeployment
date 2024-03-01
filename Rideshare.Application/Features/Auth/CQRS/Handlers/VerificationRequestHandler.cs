using System.Runtime.CompilerServices;
using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Contracts.Infrastructure;
using Rideshare.Application.Features.Auth.CQRS.Commands;
using Rideshare.Application.Features.Auth.Dtos;
using Rideshare.Application.Services;
using Rideshare.Domain.Entities;
using Rideshare_backend;

namespace Rideshare.Application.Features.Auth.CQRS.Handlers;

public class VerificationCommandHandler : IRequestHandler<VerificationCommand, BaseResponse<VerifyOtpResponse>>
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

    public async Task<BaseResponse<VerifyOtpResponse>> Handle(VerificationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<VerifyOtpResponse>(); 
        var otp = (await _otpRepository.GetByPhoneNumber(request.verifyOtpRequest.PhoneNumber));
        if (otp != null && request.verifyOtpRequest.OtpCode == otp.OtpCode)
        {
            Rider rider = await _riderRepository.GetByPhoneNUmber(request.verifyOtpRequest.PhoneNumber);
            var token = _jwtGenerator.Generate(rider);
            var verifyOtpResponse = new VerifyOtpResponse{Token = token, Id = rider.Id};
            response.Value = verifyOtpResponse;
            response.IsSuccess = true;
            response.Message = "Verified";
        }
        else
        {
            response.Message = "Not Verified";
        }
        return response;
    }
}