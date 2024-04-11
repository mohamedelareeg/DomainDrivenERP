using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CleanArchitectureWithDDD.Domain.Entities.Products;
using CleanArchitectureWithDDD.Domain.Errors;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared.Guards;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Domain.ValueObjects;

namespace CleanArchitectureWithDDD.Domain.Entities.Categories;
public class Category : AggregateRoot, IAuditableEntity
{
    private readonly List<Product> _products = new();

    public Category() { }
    private Category(Guid id, string name)
        : base(id)
    {
        Guard.Against.Null(id, nameof(id));
        Guard.Against.NullOrEmpty(name, nameof(name));

        Name = name;
    }

    public string Name { get; private set; }
    public IReadOnlyCollection<Product> Products => _products;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Result<Category> Create(string name)
    {
        var id = Guid.NewGuid();

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Category>(DomainErrors.CategoryErrors.InvalidCategoryName);
        }

        var category = new Category(id, name);
        return Result.Success(category);
    }

    public Result<Category> UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            return Result.Failure<Category>(DomainErrors.CategoryErrors.InvalidCategoryName);
        }

        Name = newName;
        return Result.Success(this);
    }
}
