using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Shared
{
    public sealed class ValidationResult : Result, IValidationResult
    {
        private ValidationResult(Error[] errors) : base(false , IValidationResult.ValidationError) { Errors = errors; }
        public Error[] Errors { get; }
        public static ValidationResult WriteErrors(Error[] errors) => new(errors);
    }
}
