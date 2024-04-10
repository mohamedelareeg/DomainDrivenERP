using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Entities.Journals.Specifications;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Journals;
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
