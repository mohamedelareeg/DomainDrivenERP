using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Domain.Entities.Journals.Specifications;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Persistence.Repositories.Journals;
internal class JournalSpecificationRepository : IJournalRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public JournalSpecificationRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateJournal(Journal journal, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Repository<Journal>().AddAsync(journal, cancellationToken);
    }

    public async Task<Journal?> GetJournalById(string journalId, CancellationToken cancellationToken = default)
    {
        BaseSpecification<Journal> spec = GetJournalByIdSpecification.GetJournalByIdSpec(journalId);

        return await _unitOfWork.Repository<Journal>().FirstOrDefaultAsync(spec, false, cancellationToken);
    }
}
