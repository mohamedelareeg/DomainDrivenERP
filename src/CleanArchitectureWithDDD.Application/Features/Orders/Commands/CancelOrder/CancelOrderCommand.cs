using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Commands.CancelOrder;
public record CancelOrderCommand(Guid OrderId) : ICommand<bool>;

