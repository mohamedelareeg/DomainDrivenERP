using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Products.DomainEvents;
public sealed record UpdateProductNameDomainEvent(Guid ProductId, string NewName) : DomainEvent(Guid.NewGuid());

