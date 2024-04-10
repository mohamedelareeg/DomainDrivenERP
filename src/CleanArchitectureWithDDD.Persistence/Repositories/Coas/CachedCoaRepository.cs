using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Coas;
internal class CachedCoaRepository : ICoaRepository
{
    private readonly ICoaRepository _decorated;
    private readonly ICacheService _cacheService;

    public CachedCoaRepository(ICoaRepository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }

    public async Task CreateCoa(COA cOA, CancellationToken cancellationToken = default)
    {
        await _decorated.CreateCoa(cOA, cancellationToken);
    }

    public async Task<string?> GetByAccountHeadCode(string accountHeadCode, CancellationToken cancellationToken = default)
    {
        string key = $"coaByHeadCode-{accountHeadCode}";

        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByAccountHeadCode(accountHeadCode, cancellationToken),
            cancellationToken);
    }

    public async Task<string?> GetByAccountName(string accountName, CancellationToken cancellationToken = default)
    {
        string key = $"accountName-{accountName}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByAccountName(accountName, cancellationToken),
            cancellationToken);
    }
    public async Task<COA?> GetCoaById(string coaId, CancellationToken cancellationToken = default)
    {
        string key = $"coa-{coaId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCoaById(coaId, cancellationToken),
            cancellationToken);
    }


    public async Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default)
    {
        string key = $"coaByName-{coaParentName}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCoaByName(coaParentName, cancellationToken),
            cancellationToken);
    }

    public async Task<List<COA>?> GetCoaChilds(string parentCoaId, CancellationToken cancellationToken = default)
    {
        string key = $"coaChilds-{parentCoaId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCoaChilds(parentCoaId, cancellationToken),
            cancellationToken);
    }

    public async Task<COA?> GetCoaWithChildren(string coaId, CancellationToken cancellationToken = default)
    {
        string key = $"coaWithChildren-{coaId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCoaWithChildren(coaId, cancellationToken),
            cancellationToken);
    }

    public async Task<string?> GetLastHeadCodeInLevelOne(CancellationToken cancellationToken = default)
    {
        string key = "lastHeadCodeLevelOne";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetLastHeadCodeInLevelOne(cancellationToken),
            cancellationToken);
    }

    public async Task<bool> IsCoaExist(string coaId, CancellationToken cancellationToken = default)
    {
        string key = $"coaExist-{coaId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.IsCoaExist(coaId, cancellationToken),
            cancellationToken);
    }

    public async Task<bool> IsCoaExist(string coaName, int level = 1, CancellationToken cancellationToken = default)
    {
        string key = $"coaExist-{coaName}-Level{level}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.IsCoaExist(coaName, level, cancellationToken),
            cancellationToken);
    }

    public async Task<bool> IsCoaExist(string coaName, string coaParentName, CancellationToken cancellationToken = default)
    {
        string key = $"coaExist-{coaName}-Parent{coaParentName}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.IsCoaExist(coaName, coaParentName, cancellationToken),
            cancellationToken);
    }

}
