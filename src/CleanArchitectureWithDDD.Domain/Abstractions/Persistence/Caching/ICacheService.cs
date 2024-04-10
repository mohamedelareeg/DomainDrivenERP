using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        //where T : class
        ;
    Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default, TimeSpan? expirationRelativeToNow = null)
        //where T : class
        ;
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T?>> valueFactory, CancellationToken cancellationToken = default, TimeSpan? expirationRelativeToNow = null)
        //where T : class
        ;


    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
    Task ClearAllCachesAsync(CancellationToken cancellationToken = default);
}
