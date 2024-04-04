using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaWithChildrens;
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
            return (Result<CoaWithChildrenDto>)Result.Failure(new Error("COA.GetCoaWithChildrensQuery", "COA not found."));
        }
        else
        {
            return _mapper.Map<CoaWithChildrenDto>(result);
        }

    }
}
