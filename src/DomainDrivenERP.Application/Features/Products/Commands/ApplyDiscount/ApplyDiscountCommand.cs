using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Products.Commands.ApplyDiscount;
public record ApplyDiscountCommand(Guid ProductId, decimal DiscountPercentage) : ICommand<decimal>;
