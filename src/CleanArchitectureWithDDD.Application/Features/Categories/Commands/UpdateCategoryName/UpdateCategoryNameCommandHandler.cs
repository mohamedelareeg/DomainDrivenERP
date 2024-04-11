using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Categories;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Categories.Commands.UpdateCategoryName;
public class UpdateCategoryNameCommandHandler : ICommandHandler<UpdateCategoryNameCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryNameCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Category>> Handle(UpdateCategoryNameCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure<Category>("Category.UpdateCategoryName", $"Category with ID {request.CategoryId} not found.");
        }
        Result<Category> updatedCategory = category.UpdateName(request.NewName);
        if (updatedCategory.IsFailure)
        {
            return Result.Failure<Category>(updatedCategory.Error);
        }
        await _categoryRepository.UpdateAsync(updatedCategory.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(updatedCategory.Value);
    }
}
