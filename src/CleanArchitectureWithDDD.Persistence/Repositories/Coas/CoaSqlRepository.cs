using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Coas;
internal class CoaSqlRepository : ICoaRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly SqlConnection _sqlConnection;

    public CoaSqlRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _sqlConnection = _connectionFactory.SqlConnection();
    }
    public async Task CreateCoa(COA cOA, CancellationToken cancellationToken = default)
    {
        await _sqlConnection.ExecuteAsync(
            "INSERT INTO COAS (HeadCode, HeadName, HeadLevel, ParentHeadCode) VALUES (@HeadCode, @HeadName, @HeadLevel, @ParentHeadCode)",
            cOA);
    }

    public async Task<COA?> GetCoaById(string coaId, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.QueryFirstOrDefaultAsync<COA>(
            "SELECT * FROM COAS WHERE HeadCode = @CoaId",
            new { CoaId = coaId });
    }

    public async Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.QueryFirstOrDefaultAsync<COA>(
            "SELECT * FROM COAS WHERE HeadName = @CoaParentName",
            new { CoaParentName = coaParentName });
    }

    public async Task<List<COA>> GetCoaChilds(string parentCoaId, CancellationToken cancellationToken = default)
    {
        return (await _sqlConnection.QueryAsync<COA>(
            "SELECT * FROM COAS WHERE ParentHeadCode = @ParentCoaId",
            new { ParentCoaId = parentCoaId })).ToList();
    }

    public async Task<COA?> GetCoaWithChildren(string coaId, CancellationToken cancellationToken = default)
    {
        var coaDictionary = new Dictionary<string, COA>();
        COA? coa = await _sqlConnection.QueryFirstOrDefaultAsync<COA>(
            "SELECT * FROM COAS WHERE HeadCode = @CoaId",
            new { CoaId = coaId });

        if (coa != null)
        {
            await FetchChildCOAs(_sqlConnection, coaDictionary, coa);
        }

        return coa;
    }

    private async Task FetchChildCOAs(IDbConnection connection, Dictionary<string, COA> coaDictionary, COA coa)
    {
        IEnumerable<COA> childCOAs = await connection.QueryAsync<COA>(
            "SELECT * FROM COAS WHERE ParentHeadCode = @ParentHeadCode",
            new { ParentHeadCode = coa.HeadCode });

        foreach (COA child in childCOAs)
        {
            if (!coaDictionary.ContainsKey(child.HeadCode))
            {
                coaDictionary[child.HeadCode] = child;
                await FetchChildCOAs(connection, coaDictionary, child);
            }
        }
        coa.InsertChildrens(childCOAs.ToList());
    }

    public async Task<bool> IsCoaExist(string coaId, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.ExecuteScalarAsync<bool>(
            "SELECT TOP 1 1 FROM COAS WHERE HeadCode = @CoaId",
            new { CoaId = coaId });
    }

    public async Task<bool> IsCoaExist(string coaName, string coaParentName, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.ExecuteScalarAsync<bool>(
            "SELECT TOP 1 1 FROM COAS c INNER JOIN COAS p ON c.ParentHeadCode = p.HeadCode WHERE c.HeadName = @CoaName AND p.HeadName = @CoaParentName",
            new { CoaName = coaName, CoaParentName = coaParentName });
    }

    public async Task<bool> IsCoaExist(string coaName, int level = 1, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.ExecuteScalarAsync<bool>(
            "SELECT TOP 1 1 FROM COAS WHERE HeadName = @CoaName AND HeadLevel = @Level",
            new { CoaName = coaName, Level = level });
    }

    public async Task<string> GetLastHeadCodeInLevelOne(CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.ExecuteScalarAsync<string>(
            "SELECT MAX(HeadCode) FROM COAS WHERE HeadLevel = 1");
    }

    public async Task<string?> GetByAccountName(string accountName, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.QueryFirstOrDefaultAsync<string>(
            "SELECT HeadCode FROM COAS WHERE HeadName = @AccountName",
            new { AccountName = accountName });
    }

    public async Task<string?> GetByAccountHeadCode(string accountHeadCode, CancellationToken cancellationToken = default)
    {
        return await _sqlConnection.QueryFirstOrDefaultAsync<string>(
            "SELECT HeadCode FROM COAS WHERE HeadCode = @AccountHeadCode",
            new { AccountHeadCode = accountHeadCode });
    }
}
