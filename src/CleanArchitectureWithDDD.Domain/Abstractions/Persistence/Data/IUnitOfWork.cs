using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;

public interface IUnitOfWork
{
    IBaseRepositoryAsync<T> Repository<T>()
        where T : BaseEntity;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
