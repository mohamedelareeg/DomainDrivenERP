using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
public interface ICoaRepository
{
    Task CreateCoa(COA cOA, CancellationToken cancellationToken = default);
    Task<COA?> GetCoaById(string coaId);
    Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default);
    Task<List<COA>> GetCoaChilds(string parentCoaId);
    Task<bool> IsCoaExist(string coaId);
    Task<bool> IsCoaExist(string coaName, int level = 1);
    Task<bool> IsCoaExist(string coaName, string coaParentName);
    Task<COA?> GetCoaWithChildren(string coaId);
    Task<string> GetLastHeadCodeInLevelOne();
}
