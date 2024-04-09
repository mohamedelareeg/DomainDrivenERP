using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Domain.ValueObjects;

/// <remarks>
/// Advantages:
/// 1. Type Safety: Ensures that only valid first names are used.
/// 2. Immutability: Once created, the first name cannot be changed.
/// 3. Encapsulation: The internal state of the first name is encapsulated, ensuring data integrity.
/// 4. Structural Equality: Supports equality comparison based on the first name value.
/// 
/// Disadvantages:
/// - Increase in Complexity: Introducing value objects adds complexity to the domain model.
///   Developers need to understand and manage value object behavior such as equality, immutability, and validation.
/// </remarks>
public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;
    private FirstName()//Require Default Constractor for the Caching
    {
        
    }
    private FirstName(string value)
    {
        Value = value;
    }
    public static Result<FirstName> Create(string firstName)
    {
        return string.IsNullOrWhiteSpace(firstName)
            ? Result.Failure<FirstName>(new Error("FirstName.Empty", "First Name is Empty"))
            : firstName.Length > MaxLength
            ? Result.Failure<FirstName>(new Error("FirstName.TooLong", "First Name is too Long"))
            : (Result<FirstName>)new FirstName(firstName);
    }
    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}
