using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Journals;
internal class CachedJournalRepository : IJournalRepository
{
    private readonly IJournalRepository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CachedJournalRepository(IJournalRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;

        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }
    public async Task CreateJournal(Journal journal, CancellationToken cancellationToken = default)
    {
        await _decorated.CreateJournal(journal, cancellationToken);
    }

    public async Task<Journal?> GetJournalById(string journalId, CancellationToken cancellationToken = default)
    {
        string key = $"journal-{journalId}";

        string? cachedJournal = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cachedJournal == null)
        {
            Journal? journal = await _decorated.GetJournalById(journalId, cancellationToken);

            if (journal != null)
            {
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(journal), _cacheOptions, cancellationToken);
            }

            return journal;
        }

        return JsonConvert.DeserializeObject<Journal>(cachedJournal);
    }
}
