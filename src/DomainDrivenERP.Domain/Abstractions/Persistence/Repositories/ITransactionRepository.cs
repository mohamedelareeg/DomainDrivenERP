using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;

public interface ITransactionRepository
{
    Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
    Task<CustomList<JournalTransactionsDto>?> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
}
