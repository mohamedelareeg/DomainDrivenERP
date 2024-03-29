using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new("ValidationError", "A Validation problem occurred.");
        Error[] Errors { get; }
    }
}
