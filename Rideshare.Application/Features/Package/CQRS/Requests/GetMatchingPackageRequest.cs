using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.Package.Dtos;
using Rideshare.Domain.Common;
using Rideshare_backend;

namespace Rideshare.Application.Features.Package.CQRS.Commands;

public class GetMatchingPackageRequest : IRequest<BaseResponse<List<GetMatchingPackageResponseDto>>>
{
    public GetMatchingPackageRequestDto GetMatchingPackageRequestDto {set; get;}
}
