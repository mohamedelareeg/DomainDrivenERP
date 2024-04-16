using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Orders.Commands.CancelOrder;
public record CancelOrderCommand(Guid OrderId) : ICommand<bool>;

