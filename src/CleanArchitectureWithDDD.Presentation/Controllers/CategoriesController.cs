using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Features.Categories.Commands.CreateCategory;
using CleanArchitectureWithDDD.Application.Features.Categories.Commands.UpdateCategoryName;
using CleanArchitectureWithDDD.Application.Features.Categories.Queries.GetCategoriesByDateRange;
using CleanArchitectureWithDDD.Application.Features.Categories.Queries.GetCategoryById;
using CleanArchitectureWithDDD.Domain.Entities.Categories;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWithDDD.Presentation.Controllers;
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
