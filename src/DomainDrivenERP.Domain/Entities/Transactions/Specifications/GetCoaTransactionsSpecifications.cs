using DomainDrivenERP.Domain.Entities.Transactions;
using DomainDrivenERP.Domain.Shared.Specifications;
using System;

namespace DomainDrivenERP.Domain.Entities.Transactions.Specifications;

public static class GetTransactionsByAccountNameSpecification
{
    public static BaseSpecification<Transaction> GetTransactionsByAccountNameSpec(string? accountName, DateTime? startDate, DateTime? endDate)
    {
        var spec = new BaseSpecification<Transaction>();
        spec.ApplyWhere(t => t.COA.HeadName == accountName);

        if (startDate != null)
        {
            spec.ApplyWhere(t => t.Journal.JournalDate >= startDate);
        }

        if (endDate != null)
        {
            spec.ApplyWhere(t => t.Journal.JournalDate <= endDate);
        }

        return spec;
    }
}
