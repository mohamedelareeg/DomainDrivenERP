using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Exceptions;
using CleanArchitectureWithDDD.Persistence.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Caching;

internal class CacheService : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        try
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (cachedValue == null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(cachedValue, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                Converters = { new ValueObjectJsonConverter() }
            });
        }
        catch (Exception ex)
        {
            throw new CacheServiceException($"Failed to get value from cache with key '{key}'.", ex);
        }
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default, TimeSpan? expirationRelativeToNow = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        try
        {
            if (_cacheOptions is null)
            {
                throw new CacheServiceException("Cache options are not properly configured.");
            }
            if (expirationRelativeToNow.HasValue)
            {
                _cacheOptions.AbsoluteExpirationRelativeToNow = expirationRelativeToNow.Value;
            }
            string cacheValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, cacheValue, _cacheOptions, cancellationToken);
            CacheKeys.TryAdd(key, false);
        }
        catch (Exception ex)
        {
            throw new CacheServiceException($"Failed to set value in cache with key '{key}'.", ex);
        }
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T?>> valueFactory, CancellationToken cancellationToken = default, TimeSpan? expirationRelativeToNow = null)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        if (valueFactory == null)
        {
            throw new ArgumentNullException(nameof(valueFactory));
        }

        try
        {
            T? cachedValue = await GetAsync<T>(key, cancellationToken);
            if (cachedValue is not null)
            {
                return cachedValue;
            }

            T? value = await valueFactory();
            if (value is not null)
            {
                await SetAsync(key, value, cancellationToken, expirationRelativeToNow);
            }

            return value;
        }
        catch (Exception ex)
        {
            throw new CacheServiceException($"Failed to get or set value in cache with key '{key}'.", ex);
        }
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        try
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
            CacheKeys.TryRemove(key, out _);
        }
        catch (Exception ex)
        {
            throw new CacheServiceException($"Failed to remove value from cache with key '{key}'.", ex);
        }
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(prefixKey))
        {
            throw new ArgumentException("Prefix key cannot be null or empty.", nameof(prefixKey));

        }

        try
        {
            IEnumerable<Task> tasks = CacheKeys.Keys
                .Where(a => a.StartsWith(prefixKey))
                .Select(k => RemoveAsync(k, cancellationToken));

            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            throw new CacheServiceException($"Failed to remove values with prefix '{prefixKey}' from cache.", ex);
        }
    }
    public async Task ClearAllCachesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await Task.WhenAll(CacheKeys.Keys.Select(key => RemoveAsync(key, cancellationToken)));
            CacheKeys.Clear();
        }
        catch (Exception ex)
        {
            throw new CacheServiceException("Failed to clear all caches.", ex);
        }
    }

}
