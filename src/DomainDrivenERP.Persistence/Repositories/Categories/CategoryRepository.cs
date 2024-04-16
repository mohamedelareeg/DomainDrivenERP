using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainDrivenERP.Persistence.Repositories.Categories;

internal class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _context.Set<Category>().AddAsync(category, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>().FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
    }

    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>().FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<CustomList<Category>> GetCategoriesByDateRangeAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Category>()
            .Where(c => c.CreatedOnUtc.Date >= fromDate.Date && c.CreatedOnUtc.Date <= toDate.Date)
            .ToCustomListAsync();
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        _context.Set<Category>().Update(category);
    }
}
