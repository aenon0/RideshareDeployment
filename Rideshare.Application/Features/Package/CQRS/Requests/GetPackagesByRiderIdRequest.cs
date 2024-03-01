using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Features.Package.Dtos;

namespace Rideshare.Application.Features.Package.CQRS.Commands;

public class GetPackagesByRiderIdRequest  : IRequest<BaseResponse<List<GetPackagesByRiderIdResponseDto>>>
{
    public GetPackagesByRiderIdRequestDto GetPackagesByRiderIdRequestDto {set; get;}
}
