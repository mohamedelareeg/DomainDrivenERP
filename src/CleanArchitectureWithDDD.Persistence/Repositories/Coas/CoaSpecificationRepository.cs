﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.COAs;
using CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
using Newtonsoft.Json.Serialization;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Coas;
internal class CoaSpecificationRepository : ICoaRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public CoaSpecificationRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateCoa(COA cOA, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.Repository<COA>().AddAsync(cOA, cancellationToken);
    }

    public async Task<string?> GetByAccountHeadCode(string accountHeadCode, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetCOAByHeadCodeSpecification.GetCOAByHeadCodeSpec(accountHeadCode);
        COA? result = await _unitOfWork.Repository<COA>().FirstOrDefaultAsync(spec, false, cancellationToken);
        return result?.HeadCode;
    }

    public async Task<string?> GetByAccountName(string accountName, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetCOAByAccountNameSpecification.GetCOAByAccountNameSpec(accountName);
        COA? result = await _unitOfWork.Repository<COA>().FirstOrDefaultAsync(spec, false, cancellationToken);
        return result?.HeadCode;
    }

    public async Task<COA?> GetCoaById(string coaId, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetCOAByHeadCodeSpecification.GetCOAByHeadCodeSpec(coaId);
        return await _unitOfWork.Repository<COA>().FirstOrDefaultAsync(spec, false, cancellationToken);
    }

    public async Task<COA?> GetCoaByName(string coaParentName, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetCOAByHeadNameSpecification.GetCOAByHeadNameSpec(coaParentName);
        return await _unitOfWork.Repository<COA>().FirstOrDefaultAsync(spec, false, cancellationToken);
    }

    public async Task<List<COA>?> GetCoaChilds(string parentCoaId, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetCOAChildsSpecification.GetCOAChildsSpec(parentCoaId);
        return (List<COA>?)await _unitOfWork.Repository<COA>().ListAsync(spec, false, cancellationToken);
    }

    public async Task<COA?> GetCoaWithChildren(string coaId, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetCOAWithChildrenSpecification.GetCOAWithChildrenSpec(coaId);
        return await _unitOfWork.Repository<COA>().FirstOrDefaultAsync(spec, false, cancellationToken);
    }

    public async Task<string?> GetLastHeadCodeInLevelOne(CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = GetLastHeadCodeInLevelOneSpecification.GetLastHeadCodeInLevelOneSpec();
        COA? result = await _unitOfWork.Repository<COA>().FirstOrDefaultAsync(spec, false, cancellationToken);
        return result?.HeadCode;
    }

    public async Task<bool> IsCoaExist(string coaId, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = IsCoaExistByIdSpecification.IsCoaExistByIdSpec(coaId);
        return await _unitOfWork.Repository<COA>().AnyAsync(spec, false, cancellationToken);
    }

    public async Task<bool> IsCoaExist(string coaName, int level = 1, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = IsCoaExistByNameAndLevelSpecification.IsCoaExistByNameAndLevelSpec(coaName, level);
        return await _unitOfWork.Repository<COA>().AnyAsync(spec, false, cancellationToken);
    }

    public async Task<bool> IsCoaExist(string coaName, string coaParentName, CancellationToken cancellationToken = default)
    {
        Domain.Specifications.BaseSpecification<COA> spec = IsCoaExistByNameAndParentNameSpecification.IsCoaExistByNameAndParentNameSpec(coaName, coaParentName);
        return await _unitOfWork.Repository<COA>().AnyAsync(spec, false, cancellationToken);
    }
}
