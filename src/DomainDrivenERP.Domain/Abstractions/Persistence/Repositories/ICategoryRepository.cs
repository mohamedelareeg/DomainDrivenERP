using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
public interface ICategoryRepository
{
    Task AddAsync(Category value, CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<CustomList<Category>> GetCategoriesByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    Task UpdateAsync(Category value, CancellationToken cancellationToken = default);
}
