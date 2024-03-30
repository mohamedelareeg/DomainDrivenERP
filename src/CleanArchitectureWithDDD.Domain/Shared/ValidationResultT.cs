namespace CleanArchitectureWithDDD.Domain.Shared;

public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{

    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError) { Errors = errors; }
    public Error[] Errors { get; }
    public static ValidationResult<TValue> WriteErrors(Error[] errors) => new(errors);
}
