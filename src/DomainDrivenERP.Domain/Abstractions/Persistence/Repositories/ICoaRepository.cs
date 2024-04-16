using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.COAs;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
public interface ICoaRepository
{
    Task CreateCoa(COA cOA, CancellationToken cancellationToken = default);
    Task<COA?> GetCoaById(string coaId, CancellationToken cancellationToken = default);
    Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default);
    Task<List<COA>?> GetCoaChilds(string parentCoaId, CancellationToken cancellationToken = default);
    Task<bool> IsCoaExist(string coaId, CancellationToken cancellationToken = default);
    Task<bool> IsCoaExist(string coaName, int level = 1, CancellationToken cancellationToken = default);
    Task<bool> IsCoaExist(string coaName, string coaParentName, CancellationToken cancellationToken = default);
    Task<COA?> GetCoaWithChildren(string coaId, CancellationToken cancellationToken = default);
    Task<string?> GetLastHeadCodeInLevelOne(CancellationToken cancellationToken = default);
    Task<string?> GetByAccountName(string accountName, CancellationToken cancellationToken = default);
    Task<string?> GetByAccountHeadCode(string accountHeadCode, CancellationToken cancellationToken = default);
}
