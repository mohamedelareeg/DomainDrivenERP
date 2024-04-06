using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Dtos;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaTransactions;
public class GetCoaTransactionsQuery : IListQuery<JournalTransactionsDto>
{
    public GetCoaTransactionsQuery(string? accountName, string? accountHeadCode, DateTime? startDate, DateTime? endDate)
    {
        AccountName = accountName;
        AccountHeadCode = accountHeadCode;
        StartDate = startDate;
        EndDate = endDate;
    }

    public string? AccountName { get; }
    public string? AccountHeadCode { get; }
    public DateTime? StartDate { get; }
    public DateTime? EndDate { get; }

}
