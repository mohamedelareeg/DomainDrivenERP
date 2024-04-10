using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Persistence.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Caching;
internal class CacheService : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        //where T : class
    {
        string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue is null)
        {
            return default;
        }
        T? value = JsonConvert.DeserializeObject<T>(cachedValue, new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateResolver(),
            Converters = { new ValueObjectJsonConverter() }
        });
        return value;
    }
    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
        //where T : class
    {
        string cacheValue = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(key, cacheValue, cancellationToken);
        CacheKeys.TryAdd(key, false);
    }
    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T?>> valueFactory, CancellationToken cancellationToken = default)
    //where T : class
    {
        T? cachedValue = await GetAsync<T>(key, cancellationToken);
        if (cachedValue is not null)
        {
            return cachedValue;
        }

        T? value = await valueFactory();
        if (value is not null)
        {
            await SetAsync(key, value, cancellationToken);
        }
        return value;

    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
       await _distributedCache.RemoveAsync(key, cancellationToken);
       CacheKeys.TryRemove(key, out bool _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        //foreach (string key in CacheKeys.Keys)
        //{
        //    if (key.StartsWith(prefixKey))
        //    {
        //        await RemoveAsync(key, cancellationToken);
        //    }
        //}
        IEnumerable<Task> tasks = CacheKeys
            .Keys
            .Where(a=>a.StartsWith(prefixKey))
            .Select( k=> RemoveAsync(k,cancellationToken));

        await Task.WhenAll(tasks);
    }

}
