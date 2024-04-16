using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Domain.Exceptions;
public class SpecificationEvaluationException : Exception
{
    public SpecificationEvaluationException(string message) : base(message) { }
    public SpecificationEvaluationException(string message, Exception innerException) : base(message, innerException) { }
}
