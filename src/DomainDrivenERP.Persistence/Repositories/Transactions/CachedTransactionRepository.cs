using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Abstractions.Persistence.Caching;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Domain.Shared.Results;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DomainDrivenERP.Persistence.Repositories.Transactions;

internal class CachedTransactionRepository : ITransactionRepository
{
    private readonly ITransactionRepository _decorated;
    private readonly ICacheService _cacheService;

    public CachedTransactionRepository(ITransactionRepository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }

    public async Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        string key = $"CoaTransactions-AccountName:{accountName}-StartDate:{startDate?.ToString("yyyyMMdd")}-EndDate:{endDate?.ToString("yyyyMMdd")}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCoaTransactionsByAccountName(accountName, startDate, endDate, cancellationToken),
            cancellationToken);
    }

    public async Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        string key = $"CoaTransactions-AccountHeadCode:{accountHeadCode}-StartDate:{startDate?.ToString("yyyyMMdd")}-EndDate:{endDate?.ToString("yyyyMMdd")}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCoaTransactionsByHeadCode(accountHeadCode, startDate, endDate, cancellationToken),
            cancellationToken);
    }
}
