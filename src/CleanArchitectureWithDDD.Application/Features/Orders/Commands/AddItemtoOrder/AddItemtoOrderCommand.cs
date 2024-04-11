using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Orders;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Commands.AddItemtoOrder;
public record AddItemtoOrderCommand(Guid OrderId, Guid ProductId, decimal ProductPrice, int Quantity) : ICommand<bool>;
