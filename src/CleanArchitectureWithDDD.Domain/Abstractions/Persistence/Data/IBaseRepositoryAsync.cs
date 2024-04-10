using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Domain.Shared.Specifications;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
public interface IBaseRepositoryAsync<T>
    where T : BaseEntity
{
    Task<IList<T>> ListAllAsync(bool allowTracking = true, CancellationToken cancellationToken = default);
    Task<IList<T>> ListAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default);
    Task<CustomList<T>> CustomListAsync(ISpecification<T> spec, bool allowTracking = true, int? totalCount = null, int? totalPages = null);
    Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default);
    Task<T> SingleAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(ISpecification<T> spec, bool allowTracking = true, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity, CancellationToken cancellationToken = default);
    void Delete(T entity, CancellationToken cancellationToken = default);
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
}
