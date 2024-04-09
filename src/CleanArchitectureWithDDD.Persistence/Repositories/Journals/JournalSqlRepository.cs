using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Domain.Entities.Journals;
using CleanArchitectureWithDDD.Domain.Entities.Transactions;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Journals;
internal class JournalSqlRepository : IJournalRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly SqlConnection _sqlConnection;

    public JournalSqlRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _sqlConnection = _connectionFactory.SqlConnection();
    }

    public async Task CreateJournal(Journal journal, CancellationToken cancellationToken = default)
    {
        JournalSnapshot snapshot = journal.ToSnapshot();
        string sql = @"
        INSERT INTO Journals (Id, JournalDate, Description)
        VALUES (@Id, @JournalDate, @Description)";

        await _sqlConnection.ExecuteAsync(sql, snapshot);
    }
    public async Task<Journal?> GetJournalById(string journalId, CancellationToken cancellationToken = default)
    {
        string sql = @"
        SELECT j.Id, j.JournalDate, j.Description,
               t.TransactionId, t.JournalId, t.COAId, t.Debit, t.Credit,
               c.Id, c.HeadCode, c.HeadName, c.HeadLevel, c.ParentHeadCode
        FROM Journals j
        LEFT JOIN Transactions t ON j.Id = t.JournalId
        LEFT JOIN COAs c ON t.COAId = c.Id
        WHERE j.Id = @JournalId";

        var journalDictionary = new Dictionary<Guid, Journal>();

        await _sqlConnection.QueryAsync<Journal, Transaction, COA, Journal>(
            sql,
            (journal, transaction, coa) =>
            {
                if (!journalDictionary.TryGetValue(journal.Id, out Journal journalEntry))
                {
                    journalEntry = Journal.FromSnapshot(new JournalSnapshot
                    {
                        Id = journal.Id,
                        Description = journal.Description,
                        IsOpening = journal.IsOpening,
                        JournalDate = journal.JournalDate,
                        Status = journal.Status
                    });
                    journalDictionary.Add(journalEntry.Id, journalEntry);
                }

                if (transaction != null)
                {
                    var transactionEntry = Transaction.FromSnapshot(new TransactionSnapshot
                    {
                        TransactionId = transaction.TransactionId,
                        JournalId = transaction.JournalId,
                        COAId = transaction.COAId,
                        Debit = transaction.Debit,
                        Credit = transaction.Credit
                    });
                    journalEntry.AddTransactions(transactionEntry);
                }

                return journalEntry;
            },
            new { JournalId = journalId },
            splitOn: "Id,TransactionId,Id");

        return journalDictionary.Values.FirstOrDefault();
    }

}
