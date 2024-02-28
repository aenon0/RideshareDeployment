using AutoMapper;
using MediatR;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Contracts.Persistence;
using Rideshare.Application.Features.Package.CQRS.Commands;
using Rideshare.Application.Features.Package.Dtos;


namespace Rideshare.Application.Features.Package.CQRS.Handlers;

public class GetPackageRequestHandler : IRequestHandler<GetPackageRequest, BaseResponse<GetPackageResponseDto>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMapper _mapper;
    public GetPackageRequestHandler(IPackageRepository packageRepository, IMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }
    public async Task<BaseResponse<GetPackageResponseDto>> Handle(GetPackageRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<GetPackageResponseDto>();
        var package = await _packageRepository.Get(request.GetPackageRequestDto.Id);
        var packageDto = _mapper.Map<GetPackageResponseDto>(package);
        response.Value = packageDto;
        return response;
    }
}

