﻿using DomainDrivenERP.Domain.Entities.Transactions;
using DomainDrivenERP.Domain.Shared.Specifications;
using System;

namespace DomainDrivenERP.Domain.Entities.Transactions.Specifications;

public static class GetTransactionsByHeadCodeSpecification
{
    public static BaseSpecification<Transaction> GetTransactionsByHeadCodeSpec(string? accountHeadCode, DateTime? startDate, DateTime? endDate)
    {
        var spec = new BaseSpecification<Transaction>();
        spec.ApplyWhere(t => t.COA.HeadCode == accountHeadCode);

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
