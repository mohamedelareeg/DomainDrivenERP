using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;

public interface ITransactionRepository
{
    Task<CustomList<JournalTransactionsDto>> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
    Task<CustomList<JournalTransactionsDto>> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken = default);
}
