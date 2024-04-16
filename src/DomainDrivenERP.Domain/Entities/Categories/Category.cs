using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Errors;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Domain.Entities.Categories;
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
