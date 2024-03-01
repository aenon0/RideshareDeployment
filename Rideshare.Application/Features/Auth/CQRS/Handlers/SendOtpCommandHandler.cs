using AutoMapper;
using MediatR;
using MongoDB.Bson;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Contracts.Infrastructure;
using Rideshare.Application.Features.Auth.CQRS.Commands;
using Rideshare.Application.Features.Auth.Dtos;
using Rideshare.Application.Services;
using Rideshare.Domain.Entities;
using Rideshare_backend;

namespace Rideshare.Application.Features.Auth.CQRS.Handlers;

public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand, BaseCommandResponse>
{
    private readonly ISmsService _smsService;
    private readonly IRiderRepository _riderRepository;
    private readonly IOtpRepository _otpRepository;
    private readonly IMapper _mapper;
    public SendOtpCommandHandler(ISmsService smsService, IRiderRepository riderRepository, IOtpRepository otpRepository, IMapper mapper)
    {
        _smsService = smsService;
        _riderRepository = riderRepository;
        _otpRepository = otpRepository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        string otpCode = new OtpGenerationService().GenerateOTP();
        try
        {
            var sentSms = await _smsService.SendSMS(request.sendOtpRequest.PhoneNumber, otpCode);
            // deal with the database once the otp is sent
            
            if (sentSms == true)// && 
            {
                var riderExists = await _riderRepository.ExistsByPhoneNumber(request.sendOtpRequest.PhoneNumber);
                //update the otp if the user is registered already
                if (riderExists)
                {
                    var existingValue = await _otpRepository.GetByPhoneNumber(request.sendOtpRequest.PhoneNumber);
                    existingValue.OtpCode = otpCode;
                    await _otpRepository.Update(existingValue);
                }
                // register them and save their otp if they are not registered
                else
                {
                    await _riderRepository.Add(new Rider { Id = new ObjectId(), PhoneNumber = request.sendOtpRequest.PhoneNumber });
                    await _otpRepository.Add(new Otp { Id = new ObjectId(), PhoneNumber = request.sendOtpRequest.PhoneNumber, OtpCode = otpCode });
                }
                var rider = await _riderRepository.GetByPhoneNUmber(request.sendOtpRequest.PhoneNumber);
                response.Id = rider.Id;
                response.Message = "OTP code has been sent successfully";
                response.IsSuccess = true;
            }
            else
            {
                response.Message = "OTP code hasn't been sent. Try again!";
            }
        }
        catch (Exception ex)
        {
            response.Errors = ex.Message.Select(c => c.ToString()).ToList();
        }
        return response;
    }
}

