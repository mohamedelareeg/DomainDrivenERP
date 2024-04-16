using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Orders.Commands.ChangeOrderStatus;
public record ChangeOrderStatusCommand(Guid OrderId, OrderStatus NewStatus) : ICommand<bool>;
