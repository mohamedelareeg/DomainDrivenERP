using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Exceptions;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.Specifications;
using CleanArchitectureWithDDD.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Data;
public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepositoryAsync(ApplicationDbContext Context)
    {
        _context = Context;
    }

    public async Task<IList<T>> ListAllAsync(bool allowTracking = true, CancellationToken cancellationToken = default)
    {
        if (allowTracking)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }
        return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IList<T>> ListAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default)
    {
        if (allowTracking)
        {
            return await ApplySpecification(spec).ToListAsync(cancellationToken);
        }
        return await ApplySpecification(spec).AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task<CustomList<T>> CustomListAsync(ISpecification<T> spec, bool allowTracking = true, int? totalCount = null, int? totalPages = null)
    {
        if (allowTracking)
        {
            return await ApplySpecification(spec).ToCustomListAsync(totalCount, totalPages);
        }
        return await ApplySpecification(spec).AsNoTracking().ToCustomListAsync(totalCount, totalPages);
    }

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default)
    {
        if (allowTracking)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
        }
        return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> SingleAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default)
    {
        T? item = (allowTracking
            ? await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken)
            : await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync(cancellationToken)) ?? throw new RecordNotFoundException(string.Format("{0} Not Found", typeof(T).Name));
        return item;
    }

    public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).CountAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }
    public async Task<bool> AnyAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default)
    {
        if (allowTracking)
        {
            return await ApplySpecification(spec).AnyAsync(cancellationToken);
        }
        return await ApplySpecification(spec).AsNoTracking().AnyAsync(cancellationToken);
    }


    public void Update(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }

 
}
