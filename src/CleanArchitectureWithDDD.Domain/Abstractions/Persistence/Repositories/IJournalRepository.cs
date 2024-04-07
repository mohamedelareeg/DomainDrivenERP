using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
public interface IJournalRepository
{
    Task CreateJournal(Journal journal);
    Task<Journal?> GetJournalById(string journalId);
}
