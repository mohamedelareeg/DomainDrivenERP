using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.COAs;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaWithChildrens;
internal class GetCoaWithChildrensQueryHandler : IQueryHandler<GetCoaWithChildrensQuery, CoaWithChildrenDto>
{
    private readonly ICoaRepository _coaRepository;
    private readonly IMapper _mapper;
    public GetCoaWithChildrensQueryHandler(ICoaRepository coaRepository, IMapper mapper)
    {
        _coaRepository = coaRepository;
        _mapper = mapper;
    }

    public async Task<Result<CoaWithChildrenDto>> Handle(GetCoaWithChildrensQuery request, CancellationToken cancellationToken)
    {
        COA? result = await _coaRepository.GetCoaWithChildren(request.HeadCode);

        if (result is null)
        {
            return Result.Failure<CoaWithChildrenDto>(new Error("COA.GetCoaWithChildrensQuery", "COA not found."));
        }
        return _mapper.Map<CoaWithChildrenDto>(result);
    }
}
