using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.Package.Dtos;
using Rideshare.Domain.Common;
using Rideshare_backend;

namespace Rideshare.Application.Features.Package.CQRS.Commands;

public class GetPackageRequest : IRequest<BaseResponse<GetPackageResponseDto>>
{
    public GetPackageRequestDto GetPackageRequestDto {set; get;}
}
