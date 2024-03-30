using CleanArchitectureWithDDD.Domain.Errors;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        public const int MaxLength = 50;
        public string Value { get; }

        private Email(string value) { Value = value; }

        public static Result<Email> Create(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Railway-Oriented Programming (ROP) approach for validating email.
            // Debug is Not Good
            // More Complex
            return  Result.Create(email)
                .Ensure(e => !string.IsNullOrWhiteSpace(e), DomainErrors.EmailErrors.Empty)
                .Ensure(e => e.Length <= MaxLength, DomainErrors.EmailErrors.TooLong)
                .Ensure(e => Regex.IsMatch(e, emailPattern), DomainErrors.EmailErrors.NotValid)
                .Map(e=> new Email(e));

            /* 
               // Traditional approach
               // In the traditional approach, each validation is performed individually, and if any validation fails, a failure result with the corresponding error is returned immediately.
               if (string.IsNullOrWhiteSpace(email))
                   return Result.Failure<Email>(DomainErrors.EmailErrors.Empty);

               if (email.Length > MaxLength)
                   return Result.Failure<Email>(DomainErrors.EmailErrors.TooLong);

               if (!Regex.IsMatch(email, emailPattern))
                   return Result.Failure<Email>(DomainErrors.EmailErrors.NotValid);

               return new Email(email);
            */

        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
