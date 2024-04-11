using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Categories;

namespace CleanArchitectureWithDDD.Application.Features.Categories.Queries.GetCategoryById;
public record GetCategoryByIdQuery(Guid CategoryId) : IQuery<Category>;
