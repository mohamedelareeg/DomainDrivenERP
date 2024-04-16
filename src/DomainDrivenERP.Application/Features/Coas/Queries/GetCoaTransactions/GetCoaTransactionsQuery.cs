using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Dtos;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaTransactions;
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
