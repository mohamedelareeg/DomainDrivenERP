using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Products;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Products;
internal class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _decorated;
    private readonly ICacheService _cacheService;

    public CachedProductRepository(IProductRepository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(product, cancellationToken);
       // await ClearProductCache(cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        string key = $"productById-{productId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByIdAsync(productId, cancellationToken),
            cancellationToken);
    }

    public async Task<Product?> GetByNameAsync(string productName, CancellationToken cancellationToken = default)
    {
        string key = $"productByName-{productName}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByNameAsync(productName, cancellationToken),
            cancellationToken);
    }

    public async Task<Product?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        string key = $"productBySku-{sku}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetProductBySkuAsync(sku, cancellationToken),
            cancellationToken);
    }

    public async Task<CustomList<Product>> GetProductsByCategoryIdAsync(Guid categoryId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        string key = $"productsByCategoryId-{categoryId}-{fromDate:yyyy-MM-dd}-{toDate:yyyy-MM-dd}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetProductsByCategoryIdAsync(categoryId, fromDate, toDate, cancellationToken),
            cancellationToken);
    }

    public async Task<CustomList<Product>> GetProductsByStockQuantityAsync(int quantity, CancellationToken cancellationToken = default)
    {
        string key = $"productsByStockQuantity-{quantity}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetProductsByStockQuantityAsync(quantity, cancellationToken),
            cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _decorated.UpdateAsync(product, cancellationToken);
        //await ClearProductCache(cancellationToken);
    }

    /*
      private async Task ClearProductCache(CancellationToken cancellationToken = default)
        {
            await _cacheService.RemoveAsync("productById-*", cancellationToken);
            await _cacheService.RemoveAsync("productByName-*", cancellationToken);
            await _cacheService.RemoveAsync("productBySku-*", cancellationToken);
            await _cacheService.RemoveAsync("productsByCategoryId-*", cancellationToken);
            await _cacheService.RemoveAsync("productsByStockQuantity-*", cancellationToken);
        }
     */
}
