﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Products;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductById;
public record GetProductByIdQuery(Guid ProductId) : IQuery<Product>;
