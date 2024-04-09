using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Transactions;

internal class CachedTransactionRepository : ITransactionRepository
{
    private readonly ITransactionRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CachedTransactionRepository(ITransactionRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;

        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }

    public async Task<CustomList<JournalTransactionsDto>> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        string key = $"CoaTransactions-AccountName:{accountName}-StartDate:{startDate?.ToString("yyyyMMdd")}-EndDate:{endDate?.ToString("yyyyMMdd")}";

        string? cachedData = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cachedData != null)
        {
            return JsonConvert.DeserializeObject<CustomList<JournalTransactionsDto>>(cachedData);
        }

        CustomList<JournalTransactionsDto> result = await _decorated.GetCoaTransactionsByAccountName(accountName, startDate, endDate, cancellationToken);
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(result), _cacheOptions, cancellationToken);

        return result;
    }

    public async Task<CustomList<JournalTransactionsDto>> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default)
    {
        string key = $"CoaTransactions-AccountHeadCode:{accountHeadCode}-StartDate:{startDate?.ToString("yyyyMMdd")}-EndDate:{endDate?.ToString("yyyyMMdd")}";

        string? cachedData = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cachedData != null)
        {
            return JsonConvert.DeserializeObject<CustomList<JournalTransactionsDto>>(cachedData);
        }

        CustomList<JournalTransactionsDto> result = await _decorated.GetCoaTransactionsByHeadCode(accountHeadCode, startDate, endDate, cancellationToken);

        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(result), _cacheOptions, cancellationToken);

        return result;
    }
}
