using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenERP.Persistence.Repositories.Products;

internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Set<Product>().AddAsync(product, cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public async Task<Product?> GetByNameAsync(string productName, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Name == productName, cancellationToken);
    }

    public async Task<Product?> GetProductBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        return await _context.Set<Product>().FirstOrDefaultAsync(p => p.SKU.Value == sku, cancellationToken);
    }

    public async Task<CustomList<Product>> GetProductsByCategoryIdAsync(Guid categoryId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>()
            .Where(p => p.CategoryId == categoryId && p.CreatedOnUtc >= fromDate && p.CreatedOnUtc <= toDate)
            .ToCustomListAsync();

    }

    public async Task<CustomList<Product>> GetProductsByStockQuantityAsync(int quantity, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Product>()
            .Where(p => p.StockQuantity < quantity)
            .ToCustomListAsync();

    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Set<Product>().Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
