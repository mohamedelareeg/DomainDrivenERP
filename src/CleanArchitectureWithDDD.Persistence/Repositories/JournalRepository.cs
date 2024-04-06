using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Repositories;
internal class JournalRepository : IJournalRepository
{
    private readonly ApplicationDbContext _context;

    public JournalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateJournal(Journal journal)
    {
        await _context.Set<Journal>().AddAsync(journal);
    }

    public async Task<Journal?> GetJournalById(string journalId)
    {
        return await _context.Set<Journal>().Include(a => a.Transactions).ThenInclude(a => a.COA).FirstOrDefaultAsync(a => a.Id.ToString() == journalId);
    }
}
