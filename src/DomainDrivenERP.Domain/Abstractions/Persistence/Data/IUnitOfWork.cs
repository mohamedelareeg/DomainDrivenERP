using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Data;

public interface IUnitOfWork
{
    IBaseRepositoryAsync<T> Repository<T>()
        where T : BaseEntity;
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
