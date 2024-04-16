using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.Journals.Specifications;
public static class GetJournalByIdSpecification
{
    public static BaseSpecification<Journal> GetJournalByIdSpec(string journalId)
    {
        var spec = new BaseSpecification<Journal>(
            j => j.Id.ToString() == journalId
        );

        spec.AddInclude(j => j.Transactions);

        spec.AddInclude("Transactions.COA");

        return spec;
    }
}
