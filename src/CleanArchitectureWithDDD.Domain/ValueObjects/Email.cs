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
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Email>(new Error("Email.Empty", "Email is Empty"));

            if (email.Length > MaxLength)
                return Result.Failure<Email>(new Error("Email.TooLong", "Email is Too Long"));

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
                return Result.Failure<Email>(new Error("Email.InvalidFormat", "Email is not in a valid format"));

            return new Email(email);
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
