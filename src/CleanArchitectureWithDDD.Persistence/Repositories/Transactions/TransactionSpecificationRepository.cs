using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Transactions;
using CleanArchitectureWithDDD.Domain.Entities.Transactions.Specifications;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Transactions;
internal class TransactionSpecificationRepository : ITransactionRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionSpecificationRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<Transaction> spec = GetTransactionsByAccountNameSpecification.GetTransactionsByAccountNameSpec(accountName, startDate, endDate);
        IList<Transaction> result = await _unitOfWork.Repository<Transaction>().ListAsync(spec, false, cancellationToken);
        return result.Select(t => new JournalTransactionsDto
        {
            TransactionId = t.TransactionId,
            JournalId = t.JournalId,
            Debit = t.Debit,
            Credit = t.Credit,
            AccountName = t.COA.HeadName,
            AccountHeadCode = t.COA.HeadCode
        }).ToCustomList();
    }

    public async Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<Transaction> spec = GetTransactionsByHeadCodeSpecification.GetTransactionsByHeadCodeSpec(accountHeadCode, startDate, endDate);
        IList<Transaction> result = await _unitOfWork.Repository<Transaction>().ListAsync(spec, false, cancellationToken);
        return result.Select(t => new JournalTransactionsDto
        {
            TransactionId = t.TransactionId,
            JournalId = t.JournalId,
            Debit = t.Debit,
            Credit = t.Credit,
            AccountName = t.COA.HeadName,
            AccountHeadCode = t.COA.HeadCode
        }).ToCustomList();
    }

}
