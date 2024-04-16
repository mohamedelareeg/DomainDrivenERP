using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Categories;

namespace DomainDrivenERP.Application.Features.Categories.Queries.GetCategoryById;
public record GetCategoryByIdQuery(Guid CategoryId) : IQuery<Category>;
