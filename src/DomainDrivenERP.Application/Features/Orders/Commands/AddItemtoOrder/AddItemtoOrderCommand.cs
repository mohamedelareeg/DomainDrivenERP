using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Orders.Commands.AddItemtoOrder;
public record AddItemtoOrderCommand(Guid OrderId, Guid ProductId, decimal ProductPrice, int Quantity) : ICommand<bool>;
