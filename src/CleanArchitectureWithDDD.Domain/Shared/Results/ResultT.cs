using System.Net;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Domain.Shared.Results;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error, HttpStatusCode statusCode) : base(isSuccess, error, statusCode)
    {
        _value = value;
    }

    public TValue Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException("The Value of a failure result cannot be accessed.");
            }
            return _value!;
        }
    }

    public static implicit operator Result<TValue>(TValue? value) => value != null ? Success(value) : throw new ArgumentNullException(nameof(value));
}
