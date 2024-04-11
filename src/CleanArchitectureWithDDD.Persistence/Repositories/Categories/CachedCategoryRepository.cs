using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Categories;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Categories;

internal class CachedCategoryRepository : ICategoryRepository
{
    private readonly ICategoryRepository _decorated;
    private readonly ICacheService _cacheService;

    public CachedCategoryRepository(ICategoryRepository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }

    public async Task AddAsync(Category value, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(value, cancellationToken);
        //await ClearCategoryCache(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        string key = $"categoryById-{categoryId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByIdAsync(categoryId, cancellationToken),
            cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        string key = $"categoryByName-{name}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByNameAsync(name, cancellationToken),
            cancellationToken);
    }

    public async Task<CustomList<Category>> GetCategoriesByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        string key = $"categoriesByDateRange-{fromDate:yyyy-MM-dd}-{toDate:yyyy-MM-dd}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCategoriesByDateRangeAsync(fromDate, toDate, cancellationToken),
            cancellationToken);
    }

    public async Task UpdateAsync(Category value, CancellationToken cancellationToken = default)
    {
        await _decorated.UpdateAsync(value, cancellationToken);
        //await ClearCategoryCache(cancellationToken);
    }

    /* Example of how to clear added/updated item cache
    private async Task ClearCategoryCache(CancellationToken cancellationToken = default)
    {
        await _cacheService.RemoveAsync("categoryById-*", cancellationToken);
        await _cacheService.RemoveAsync("categoryByName-*", cancellationToken);
    }
    */
}
