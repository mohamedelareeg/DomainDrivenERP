using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CachedCoaRepository(ICoaRepository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;

        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }

    public async Task CreateCoa(COA cOA, CancellationToken cancellationToken = default)
    {
        await _decorated.CreateCoa(cOA, cancellationToken);
    }

    public async Task<string?> GetByAccountHeadCode(string accountHeadCode, CancellationToken cancellationToken = default)
    {
        string key = $"coaByHeadCode-{accountHeadCode}";
        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            string? result = await _decorated.GetByAccountHeadCode(accountHeadCode, cancellationToken);

            if (result != null)
            {
                await _distributedCache.SetStringAsync(key, result, _cacheOptions, cancellationToken);
            }

            return result;
        }
        else
        {
            return cachedValue;
        }
    }

    public async Task<string?> GetByAccountName(string accountName, CancellationToken cancellationToken = default)
    {
        string key = $"accountName-{accountName}";
        string? cachedAccountHeadCode = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (!string.IsNullOrEmpty(cachedAccountHeadCode))
        {
            return cachedAccountHeadCode;
        }
        string? accountHeadCode = await _decorated.GetByAccountName(accountName, cancellationToken);

        if (accountHeadCode != null)
        {
            await _distributedCache.SetStringAsync(key, accountHeadCode, _cacheOptions, cancellationToken);
        }

        return accountHeadCode;
    }
    public async Task<COA?> GetCoaById(string coaId, CancellationToken cancellationToken = default)
    {
        string key = $"coa-{coaId}";

        string? cachedCoa = await _distributedCache.GetStringAsync(key, cancellationToken);

        COA? coa;
        if (string.IsNullOrEmpty(cachedCoa))
        {
            coa = await _decorated.GetCoaById(coaId, cancellationToken);
            if (coa == null)
            {
                return coa;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(coa), _cacheOptions, cancellationToken);

            return coa;
        }
        coa = JsonConvert.DeserializeObject<COA>(cachedCoa);
        return coa;
    }


    public async Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default)
    {
        string key = $"coaByName-{coaParentName}";

        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            COA? result = await _decorated.GetCoaByName(coaParentName, cancellationToken);

            if (result != null)
            {
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(result), _cacheOptions, cancellationToken);
            }

            return result;
        }
        else
        {
            return JsonConvert.DeserializeObject<COA>(cachedValue);
        }
    }

    public async Task<List<COA>> GetCoaChilds(string parentCoaId, CancellationToken cancellationToken = default)
    {
        string key = $"coaChilds-{parentCoaId}";

        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            List<COA> result = await _decorated.GetCoaChilds(parentCoaId, cancellationToken);

            if (result != null)
            {
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(result), _cacheOptions, cancellationToken);
            }

            return result;
        }
        else
        {
            return JsonConvert.DeserializeObject<List<COA>>(cachedValue);
        }
    }

    public async Task<COA?> GetCoaWithChildren(string coaId, CancellationToken cancellationToken = default)
    {
        string key = $"coaWithChildren-{coaId}";

        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            COA? result = await _decorated.GetCoaWithChildren(coaId, cancellationToken);

            if (result != null)
            {
                await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(result), _cacheOptions, cancellationToken);
            }

            return result;
        }
        else
        {
            return JsonConvert.DeserializeObject<COA>(cachedValue);
        }
    }

    public async Task<string> GetLastHeadCodeInLevelOne(CancellationToken cancellationToken = default)
    {
        string key = "lastHeadCodeLevelOne";
        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            string result = await _decorated.GetLastHeadCodeInLevelOne(cancellationToken);
            await _distributedCache.SetStringAsync(key, result, _cacheOptions, cancellationToken);

            return result;
        }
        else
        {
            return cachedValue;
        }
    }

    public async Task<bool> IsCoaExist(string coaId, CancellationToken cancellationToken = default)
    {
        string key = $"coaExist-{coaId}";

        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            bool result = await _decorated.IsCoaExist(coaId, cancellationToken);
            await _distributedCache.SetStringAsync(key, result.ToString(), _cacheOptions, cancellationToken);

            return result;
        }
        else
        {
            return bool.Parse(cachedValue);
        }
    }


    public async Task<bool> IsCoaExist(string coaName, int level = 1, CancellationToken cancellationToken = default)
    {
        string key = $"coaExist-{coaName}-Level{level}";
        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            bool result = await _decorated.IsCoaExist(coaName, level, cancellationToken);

            await _distributedCache.SetStringAsync(key, result.ToString(), _cacheOptions, cancellationToken);


            return result;
        }
        else
        {
            return bool.Parse(cachedValue);
        }
    }


    public async Task<bool> IsCoaExist(string coaName, string coaParentName, CancellationToken cancellationToken = default)
    {
        string key = $"coaExist-{coaName}-Parent{coaParentName}";

        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            bool result = await _decorated.IsCoaExist(coaName, coaParentName, cancellationToken);
            await _distributedCache.SetStringAsync(key, result.ToString(), _cacheOptions, cancellationToken);

            return result;
        }
        else
        {
            return bool.Parse(cachedValue);
        }
    }

}
