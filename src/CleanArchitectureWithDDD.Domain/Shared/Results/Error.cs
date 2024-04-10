namespace CleanArchitectureWithDDD.Domain.Shared.Results;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new Error(string.Empty, string.Empty);
    public static readonly Error NullValue = new Error("Error.NullValue", "The Specified Result Value is null.");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    public override bool Equals(object obj)
    {
        if (obj is Error other)
        {
            return Equals(other);
        }
        return false;
    }

    public bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }
}
