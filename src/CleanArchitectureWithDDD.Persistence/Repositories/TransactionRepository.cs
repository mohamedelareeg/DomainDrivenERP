using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CleanArchitectureWithDDD.Persistence.Repositories;

internal class TransactionRepository : ITransactionRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public TransactionRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<CustomList<JournalTransactionsDto>> GetCoaTransactionsByAccountName(string? accountName, DateTime? startDate, DateTime? endDate)
    {
        await using SqlConnection sqlConnection = _connectionFactory.SqlConnection();
        const string sql = @"
        SELECT t.TransactionId, t.JournalId, t.Debit, t.Credit, c.HeadName AS AccountName, c.HeadCode AS AccountHeadCode
        FROM Transactions t 
        INNER JOIN Coas c ON t.COAId = c.HeadCode 
        INNER JOIN Journals j ON t.JournalId = j.Id
        WHERE c.HeadName = @AccountName   
        AND (@StartDate IS NULL OR j.JournalDate >= @StartDate)
        AND (@EndDate IS NULL OR j.JournalDate <= @EndDate)";

        IEnumerable<JournalTransactionsDto> result = await sqlConnection.QueryAsync<JournalTransactionsDto>(
            sql,
            new { AccountName = accountName, StartDate = startDate, EndDate = endDate });

        return result.ToCustomList();
    }


    public async Task<CustomList<JournalTransactionsDto>> GetCoaTransactionsByHeadCode(string? accountHeadCode, DateTime? startDate, DateTime? endDate)
    {
        await using SqlConnection sqlConnection = _connectionFactory.SqlConnection();
        const string sql = @"
            SELECT t.TransactionId, t.JournalId, t.Debit, t.Credit, c.HeadName AS AccountName, c.HeadCode AS AccountHeadCode
            FROM Transactions t 
            INNER JOIN Coas c ON t.COAId = c.HeadCode 
            INNER JOIN Journals j ON t.JournalId = j.Id
            WHERE t.COAId = @AccountHeadCode
            AND (@StartDate IS NULL OR j.JournalDate >= @StartDate)
            AND (@EndDate IS NULL OR j.JournalDate <= @EndDate)";

        IEnumerable<JournalTransactionsDto> result = await sqlConnection.QueryAsync<JournalTransactionsDto>(
            sql,
            new { AccountHeadCode = accountHeadCode, StartDate = startDate, EndDate = endDate });

        return result.ToCustomList();
    }
}
