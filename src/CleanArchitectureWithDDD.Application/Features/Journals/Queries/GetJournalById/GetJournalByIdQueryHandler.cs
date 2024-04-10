using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Journals.Queries.GetJournalById;
internal class GetJournalByIdQueryHandler : IQueryHandler<GetJournalByIdQuery, JournalDto>
{
    private readonly IJournalRepository _journalRepository;
    private readonly IMapper _mapper;

    public GetJournalByIdQueryHandler(IJournalRepository journalRepository, IMapper mapper)
    {
        _journalRepository = journalRepository;
        _mapper = mapper;
    }

    public async Task<Result<JournalDto>> Handle(GetJournalByIdQuery request, CancellationToken cancellationToken)
    {
        Journal? result = await _journalRepository.GetJournalById(request.JournalId);

        if (result is null)
        {
            return Result.Failure<JournalDto>(new Error("Journal.GetJournalByIdQuery", "Journal not found."));
        }
        return _mapper.Map<JournalDto>(result);
    }
}
