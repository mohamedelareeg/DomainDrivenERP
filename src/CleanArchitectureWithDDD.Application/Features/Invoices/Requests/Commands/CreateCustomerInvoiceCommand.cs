using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Commands
{
    public class CreateCustomerInvoiceCommand : IRequest<bool>
    {
    }
}
