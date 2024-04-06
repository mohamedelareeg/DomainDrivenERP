using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;

public interface ITransactionRepository
{
    Task<List<JournalTransactionsDto>> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate);
    Task<List<JournalTransactionsDto>> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate);
}
