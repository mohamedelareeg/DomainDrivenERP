using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Persistence.Clients;
using CleanArchitectureWithDDD.Persistence.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Coa;
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

    public async Task<COA?> GetCoaById(string coaId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().FirstOrDefaultAsync(a => a.HeadCode == coaId);
    }

    public async Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().Include(a => a.COAs).FirstOrDefaultAsync(a => a.HeadName == coaParentName);
    }

    public async Task<List<COA>?> GetCoaChilds(string parentCoaId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().Where(a => a.ParentHeadCode == parentCoaId).ToListAsync();
    }

    public async Task<COA?> GetCoaWithChildren(string coaId, CancellationToken cancellationToken = default)
    {
        COA? coa = await _context.Set<COA>()
            .FirstOrDefaultAsync(c => c.HeadCode == coaId);

        if (coa != null)
        {
            await LoadChildrenRecursively(coa);
        }

        return coa;
    }
    public async Task<bool> IsCoaExist(string coaId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().AnyAsync(coa => coa.HeadCode == coaId, cancellationToken);
    }

    public async Task<bool> IsCoaExist(string coaName, string coaParentName, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().Include(a => a.ParentCOA)
            .AnyAsync(coa => coa.HeadName == coaName && coa.ParentCOA.HeadName == coaParentName);
    }

    public async Task<bool> IsCoaExist(string coaName, int level = 1, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>()
             .AnyAsync(coa => coa.HeadName == coaName && coa.HeadLevel == level);
    }

    public async Task<string?> GetLastHeadCodeInLevelOne(CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>().Where(a => a.HeadLevel == 1).MaxAsync(coa => coa.HeadCode);
    }

    public async Task<string?> GetByAccountName(string accountName, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>()
                             .Where(coa => coa.HeadName == accountName)
                             .Select(coa => coa.HeadCode)
                             .FirstOrDefaultAsync();
    }

    public async Task<string?> GetByAccountHeadCode(string accountHeadCode, CancellationToken cancellationToken = default)
    {
        return await _context.Set<COA>()
                             .Where(coa => coa.HeadCode == accountHeadCode)
                             .Select(coa => coa.HeadCode)
                             .FirstOrDefaultAsync();
    }


    private async Task LoadChildrenRecursively(COA coa)
    {
        await _context.Entry(coa)
            .Collection(c => c.COAs)
            .LoadAsync();

        foreach (COA? child in coa.COAs.ToList())
        {
            await LoadChildrenRecursively(child);
        }
    }
}
