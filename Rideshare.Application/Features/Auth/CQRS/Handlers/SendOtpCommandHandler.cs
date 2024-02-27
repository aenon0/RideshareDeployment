using MediatR;
using Rideshare.Application.Contracts.Infrastructure;
using Rideshare.Application.Features.Auth.CQRS.Commands;
using Rideshare.Application.Models.Requests;
using Rideshare.Application.Models.Responses;
using Rideshare.Application.Services;
using Rideshare.Domain.Entities;
using Rideshare_backend;

namespace Rideshare.Application.Features.Auth.CQRS.Handlers;



public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, SendOtpResponse>
{
    private readonly ISmsService _smsService;
    private readonly IRiderRepository _riderRepository;
    private readonly IOtpRepository _otpRepository;
    public SendOtpCommandHandler(ISmsService smsService, IRiderRepository riderRepository, IOtpRepository otpRepository)
    {
        _smsService = smsService;
        _riderRepository = riderRepository;
        _otpRepository = otpRepository;
    }

    public async Task<SendOtpResponse> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        string otpCode = new OtpGenerationService().GenerateOTP();
        await _smsService.SendSMS(request.sendOtpRequest.PhoneNumber, otpCode);

        var riderExists = await _riderRepository.ExistsByPhoneNumber(request.sendOtpRequest.PhoneNumber);
        if(riderExists)
        {
            var existingValue = await _otpRepository.GetByPhoneNumber(request.sendOtpRequest.PhoneNumber);
            existingValue.PhoneNumber = request.sendOtpRequest.PhoneNumber;
            await _otpRepository.Update(existingValue);
        }
        // register them and save their otp if they are not registered
        else{
            await _riderRepository.Add(new Rider{Id = Guid.NewGuid().ToString(), PhoneNumber = request.sendOtpRequest.PhoneNumber});
            await _otpRepository.Add(new Otp{Id = Guid.NewGuid().ToString(), PhoneNumber = request.sendOtpRequest.PhoneNumber, OtpCode = otpCode});
        }
        return new SendOtpResponse{message = $"OTP code has been sent successfully"};
    }
}

