using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Features.Categories.Commands.CreateCategory;
using DomainDrivenERP.Application.Features.Categories.Commands.UpdateCategoryName;
using DomainDrivenERP.Application.Features.Categories.Queries.GetCategoriesByDateRange;
using DomainDrivenERP.Application.Features.Categories.Queries.GetCategoryById;
using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[Route("api/v1/categories")]
public class CategoriesController : AppControllerBase
{
    public CategoriesController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Result<Category> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPut("update-name")]
    public async Task<IActionResult> UpdateCategoryName(UpdateCategoryNameCommand request, CancellationToken cancellationToken)
    {
        Result<Category> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("by-date-range")]
    public async Task<IActionResult> GetCategoriesByDateRange(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        Result<CustomList<Category>> result = await Sender.Send(new GetCategoriesByDateRangeQuery(fromDate, toDate), cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId, CancellationToken cancellationToken)
    {
        Result<Category> result = await Sender.Send(new GetCategoryByIdQuery(categoryId), cancellationToken);
        return CustomResult(result);
    }
}
