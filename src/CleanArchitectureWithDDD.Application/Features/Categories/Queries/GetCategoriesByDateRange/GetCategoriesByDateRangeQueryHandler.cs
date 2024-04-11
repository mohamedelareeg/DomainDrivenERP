using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Categories;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Categories.Queries.GetCategoriesByDateRange;
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
