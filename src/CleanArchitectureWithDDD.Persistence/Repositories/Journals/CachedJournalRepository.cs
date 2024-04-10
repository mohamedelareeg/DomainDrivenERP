using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
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
    private readonly ICacheService _cacheService;

    public CachedJournalRepository(IJournalRepository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }
    public async Task CreateJournal(Journal journal, CancellationToken cancellationToken = default)
    {
        await _decorated.CreateJournal(journal, cancellationToken);
    }

    public async Task<Journal?> GetJournalById(string journalId, CancellationToken cancellationToken = default)
    {
        string key = $"journal-{journalId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetJournalById(journalId, cancellationToken),
            cancellationToken);
    }
}
