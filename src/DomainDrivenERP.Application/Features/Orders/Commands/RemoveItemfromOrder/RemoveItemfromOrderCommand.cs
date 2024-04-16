using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Orders.Commands.RemoveItemfromOrder;
public record RemoveItemfromOrderCommand(Guid OrderId, Guid LineItemId) : ICommand<bool>;
