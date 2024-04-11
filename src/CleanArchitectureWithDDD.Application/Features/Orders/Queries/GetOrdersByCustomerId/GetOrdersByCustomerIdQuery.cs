using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Orders;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Queries.GetOrdersByCustomerId;
public record GetOrdersByCustomerIdQuery(Guid CustomerId, DateTime FromDate, DateTime ToDate) : IListQuery<Order>;
