using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.ValueObjects
{
    /// <remarks>
    /// Advantages:
    /// 1. Type Safety: Ensures that only valid last names are used.
    /// 2. Immutability: Once created, the last name cannot be changed.
    /// 3. Encapsulation: The internal state of the last name is encapsulated, ensuring data integrity.
    /// 4. Structural Equality: Supports equality comparison based on the last name value.
    /// 
    /// Disadvantages:
    /// - Increase in Complexity: Introducing value objects adds complexity to the domain model.
    ///   Developers need to understand and manage value object behavior such as equality, immutability, and validation.
    /// </remarks>
    public sealed class LastName : ValueObject
    {
        public const int MaxLength = 50;
        private LastName(string value)
        {
            Value = value;
        }
        public static Result<LastName> Create(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return Result.Failure<LastName>(new Error("LastName.Empty", "Last Name is Empty"));
            }
            if(lastName.Length > MaxLength) {
                return Result.Failure<LastName>(new Error("LastName.TooLong", "Last Name is too Long"));
            }
            return new LastName(lastName);
        }
        public string Value { get; }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

    }
}
