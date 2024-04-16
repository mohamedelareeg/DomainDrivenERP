﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Orders.Queries.GetOrdersByCustomerId;
public record GetOrdersByCustomerIdQuery(Guid CustomerId, DateTime FromDate, DateTime ToDate) : IListQuery<Order>;
