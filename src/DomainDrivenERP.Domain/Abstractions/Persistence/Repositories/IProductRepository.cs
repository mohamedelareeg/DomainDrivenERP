using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
public interface IProductRepository
{
    Task AddAsync(Product value, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<Product?> GetByNameAsync(string productName, CancellationToken cancellationToken = default);
    Task<Product?> GetProductBySkuAsync(string sKU, CancellationToken cancellationToken);
    Task<CustomList<Product>> GetProductsByCategoryIdAsync(Guid categoryId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task<CustomList<Product>> GetProductsByStockQuantityAsync(int quantity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
}
