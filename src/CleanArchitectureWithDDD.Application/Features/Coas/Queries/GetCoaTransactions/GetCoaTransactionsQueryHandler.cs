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

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaTransactions;
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
