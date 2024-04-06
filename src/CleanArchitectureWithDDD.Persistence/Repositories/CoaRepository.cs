using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Repositories;
internal sealed class CoaRepository : ICoaRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ISqlConnectionFactory _connectionFactory;
    public CoaRepository(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
    {
        _context = context;
        _connectionFactory = connectionFactory;
    }

    public async Task CreateCoa(COA cOA, CancellationToken cancellationToken = default)
    {
        await _context.Set<COA>().AddAsync(cOA);
    }

    public async Task<COA?> GetCoaById(string coaId)
    {
        return await _context.Set<COA>().FirstOrDefaultAsync(a => a.HeadCode == coaId);
    }

    public async Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().Include(a=>a.COAs).FirstOrDefaultAsync(a=>a.HeadName == coaParentName);
    }

    public async Task<List<COA>> GetCoaChilds(string parentCoaId)
    {
        return await _context.Set<COA>().Where(a => a.ParentHeadCode == parentCoaId).ToListAsync();
    }

    public async Task<COA?> GetCoaWithChildren(string coaId)
    {
        await using SqlConnection sqlConnection = _connectionFactory.SqlConnection();

        var coaDictionary = new Dictionary<string, COA>();
        COA? coa = await sqlConnection.QueryFirstOrDefaultAsync<COA>(
            "SELECT * FROM COAS WHERE HeadCode = @CoaId",
            new { CoaId = coaId });

        if (coa != null)
        {
            await FetchChildCOAs(sqlConnection, coaDictionary, coa);
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
    public async Task<bool> IsCoaExist(string coaId)
    {
        return await _context.Set<COA>().AnyAsync(coa => coa.HeadCode == coaId);
    }

    public async Task<bool> IsCoaExist(string coaName, string coaParentName)
    {
        return await _context.Set<COA>().Include(a => a.ParentCOA)
            .AnyAsync(coa => coa.HeadName == coaName && coa.ParentCOA.HeadName == coaParentName);
    }

    public async Task<bool> IsCoaExist(string coaName, int level = 1)
    {
        return await _context.Set<COA>()
             .AnyAsync(coa => coa.HeadName == coaName && coa.HeadLevel == level);
    }

    public async Task<string> GetLastHeadCodeInLevelOne()
    {
        return await _context.Set<COA>().Where(a => a.HeadLevel == 1).MaxAsync(coa => coa.HeadCode);
    }

    public async Task<string?> GetByAccountName(string accountName)
    {
        return await _context.Set<COA>()
                             .Where(coa => coa.HeadName == accountName)
                             .Select(coa => coa.HeadCode)
                             .FirstOrDefaultAsync();
    }

    public async Task<string?> GetByAccountHeadCode(string accountHeadCode)
    {
        return await _context.Set<COA>()
                             .Where(coa => coa.HeadCode == accountHeadCode)
                             .Select(coa => coa.HeadCode)
                             .FirstOrDefaultAsync();
    }

}
