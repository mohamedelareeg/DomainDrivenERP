namespace CleanArchitectureWithDDD.Domain.Shared;

public sealed class ValidationResult : Result, IValidationResult
{
    private ValidationResult(Error[] errors) : base(false, IValidationResult.ValidationError,System.Net.HttpStatusCode.BadRequest) { Errors = errors; }
    public Error[] Errors { get; }
    public static ValidationResult WriteErrors(Error[] errors) => new(errors);
}
