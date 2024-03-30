using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Queries.RetriveCustomerInvoice
{
    public class RetriveCustomerInvoiceQuery : IQuery<bool>
    {
    }
}
