using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Categories.Queries.GetCategoriesByDateRange;
public class GetCategoriesByDateRangeQueryHandler : IListQueryHandler<GetCategoriesByDateRangeQuery, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesByDateRangeQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CustomList<Category>>> Handle(GetCategoriesByDateRangeQuery request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetCategoriesByDateRangeAsync(request.FromDate, request.ToDate, cancellationToken);
    }

}
