using MediatR;
using Rideshare.Application.Features.Package.CQRS.Commands;
using Rideshare.Application.Features.Package.Dtos;
using Rideshare.Application.Common.Response;
using Rideshare.Application.Contracts.Persistence;
using AutoMapper;
using Rideshare.Domain.Entities;

namespace Rideshare.Application.Features.Package.CQRS.Handlers;

public class GetPackagesByRiderIdRequestHandler : IRequestHandler<GetPackagesByUserIdRequest, BaseResponse<List<GetPackagesByRiderIdResponseDto>>>
{
    private readonly IRiderHistoryRepository _riderHistoryRepository;
    private readonly IMapper _mapper;
    public GetPackagesByRiderIdRequestHandler(IRiderHistoryRepository riderHistoryRepository, IMapper mapper)
    {
        _riderHistoryRepository = riderHistoryRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<GetPackagesByRiderIdResponseDto>>> Handle(GetPackagesByUserIdRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<List<GetPackagesByRiderIdResponseDto>>();
        var riderHistory = await _riderHistoryRepository.GetByUserId(request.GetPackagesByUserIdRequestDto.UserId);
        var packageDto = _mapper.Map<List<GetPackagesByRiderIdResponseDto>>(riderHistory);
        response.Value = packageDto;
        return response;
    }
}


