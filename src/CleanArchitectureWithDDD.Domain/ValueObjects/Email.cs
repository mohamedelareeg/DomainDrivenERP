using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private const int MaxLength = 50;
        public string Value { get; }
        private Email(string value) { Value = value; }
        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return Result.Failure<Email>(new Error("Email.Empty", "Email is Empty"));
            if (email.Length > MaxLength) return Result.Failure<Email>(new Error("Email.TooLong", "Email is Too Long"));
            return new Email(email);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
