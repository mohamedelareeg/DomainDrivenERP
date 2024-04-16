using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaTransactions;
internal class GetCoaTransactionsQueryHandler : IListQueryHandler<GetCoaTransactionsQuery, JournalTransactionsDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public GetCoaTransactionsQueryHandler(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }

    public async Task<Result<CustomList<JournalTransactionsDto>>> Handle(GetCoaTransactionsQuery request, CancellationToken cancellationToken)
    {
        CustomList<JournalTransactionsDto> transactionsList = string.IsNullOrEmpty(request.AccountName)
            ? await _transactionRepository.GetCoaTransactionsByHeadCode(request.AccountHeadCode, request.StartDate, request.EndDate)
            : await _transactionRepository.GetCoaTransactionsByAccountName(request.AccountName, request.StartDate, request.EndDate);

        return transactionsList;

    }

}
