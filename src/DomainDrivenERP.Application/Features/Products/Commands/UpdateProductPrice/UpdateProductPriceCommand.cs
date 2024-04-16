using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Products.Commands.UpdateProductPrice;
public record UpdateProductPriceCommand(Guid ProductId, decimal NewPriceAmount, string Currency) : ICommand<bool>;
