using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Categories;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Category>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? existingCategory = await _categoryRepository.GetByNameAsync(request.Name, cancellationToken);
        if (existingCategory is not null)
        {
            return Result.Failure<Category>("Category.CreateCategory", $"Category '{request.Name}' already exists with the same name.");
        }

        Result<Category> categoryResult = Category.Create(request.Name);
        if (categoryResult.IsFailure)
        {
            return Result.Failure<Category>(categoryResult.Error);
        }

        await _categoryRepository.AddAsync(categoryResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(categoryResult.Value);
    }
}
