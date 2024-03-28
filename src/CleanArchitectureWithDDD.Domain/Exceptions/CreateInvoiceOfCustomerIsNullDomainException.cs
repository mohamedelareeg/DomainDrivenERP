using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Exceptions
{
    public sealed class CreateInvoiceOfCustomerIsNullDomainException : DomainException
    {
        public CreateInvoiceOfCustomerIsNullDomainException(string message):base(message) { }
    }
}
