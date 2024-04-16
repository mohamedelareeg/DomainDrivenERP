using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenERP.Persistence.Repositories.Journals;
internal class JournalRepository : IJournalRepository
{
    private readonly ApplicationDbContext _context;

    public JournalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateJournal(Journal journal, CancellationToken cancellationToken = default)
    {
        await _context.Set<Journal>().AddAsync(journal, cancellationToken);
    }

    public async Task<Journal?> GetJournalById(string journalId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Journal>().Include(a => a.Transactions).ThenInclude(a => a.COA).FirstOrDefaultAsync(a => a.Id.ToString() == journalId, cancellationToken);
    }
}
