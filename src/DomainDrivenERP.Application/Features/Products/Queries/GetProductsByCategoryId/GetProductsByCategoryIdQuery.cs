using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Products;

namespace DomainDrivenERP.Application.Features.Products.Queries.GetProductsByCategoryId;
public record GetProductsByCategoryIdQuery(Guid CategoryId, DateTime FromDate, DateTime ToDate) : IListQuery<Product>;
