using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities;
using DomainDrivenERP.Domain.Entities.Transactions;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Persistence.Clients;
using DomainDrivenERP.Persistence.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenERP.Persistence.Repositories.Transactions;

internal class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        IQueryable<JournalTransactionsDto> query = _context.Set<Transaction>()
            .Where(t => t.COA.HeadName == accountName &&
                        (startDate == null || t.Journal.JournalDate >= startDate) &&
                        (endDate == null || t.Journal.JournalDate <= endDate))
            .Select(t => new JournalTransactionsDto
            {
                TransactionId = t.TransactionId,
                JournalId = t.JournalId,
                Debit = t.Debit,
                Credit = t.Credit,
                AccountName = t.COA.HeadName,
                AccountHeadCode = t.COA.HeadCode
            });

        List<JournalTransactionsDto> result = await query.ToListAsync(cancellationToken);
        return result.ToCustomList();
    }

    public async Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        IQueryable<JournalTransactionsDto> query = _context.Set<Transaction>()
            .Where(t => t.COA.HeadCode == accountHeadCode &&
                        (startDate == null || t.Journal.JournalDate >= startDate) &&
                        (endDate == null || t.Journal.JournalDate <= endDate))
            .Select(t => new JournalTransactionsDto
            {
                TransactionId = t.TransactionId,
                JournalId = t.JournalId,
                Debit = t.Debit,
                Credit = t.Credit,
                AccountName = t.COA.HeadName,
                AccountHeadCode = t.COA.HeadCode
            });

        List<JournalTransactionsDto> result = await query.ToListAsync(cancellationToken);
        return result.ToCustomList();
    }
}
