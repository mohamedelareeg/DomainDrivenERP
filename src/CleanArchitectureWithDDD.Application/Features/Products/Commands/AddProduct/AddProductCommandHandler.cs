using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Products;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Product>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Categories.Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
        {
            return Result.Failure<Product>("Product.AddProduct", "Category not found");
        }
        Product? existingProduct = await _productRepository.GetByNameAsync(request.ProductName, cancellationToken);
        if (existingProduct != null)
        {
            return Result.Failure<Product>("Product.AddProduct", "A product with the same name already exists");
        }
        Result<Product> product = Product.Create(request.ProductName, request.Amount, request.Currency, request.StockQuantity, request.SKU, request.Model, request.Details, request.CategoryId);
        if (product.IsFailure)
        {
            return Result.Failure<Product>(product.Error);
        }

        await _productRepository.AddAsync(product.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(product.Value);
    }
}
