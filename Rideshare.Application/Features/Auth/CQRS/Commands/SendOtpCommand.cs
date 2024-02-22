using MediatR;
using Rideshare.Application.Models.Requests;
using Rideshare.Application.Models.Responses;

namespace Rideshare.Application.Features.Auth.CQRS.Commands;

public class SendOtpCommand: IRequest<SendOtpResponse>
{
     public SendOtpRequest sendOtpRequest {get; set;}
}
