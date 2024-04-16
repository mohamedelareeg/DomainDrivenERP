using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Domain.Shared;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
public interface IJournalRepository
{
    Task CreateJournal(Journal journal, CancellationToken cancellationToken = default);
    Task<Journal?> GetJournalById(string journalId, CancellationToken cancellationToken = default);
}
