using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Domain.Exceptions;
public class CacheServiceException : Exception
{
    public CacheServiceException(string message) : base(message) { }
    public CacheServiceException(string message, Exception innerException) : base(message, innerException) { }
}
