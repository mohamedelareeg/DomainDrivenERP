namespace DomainDrivenERP.Domain.Shared.Results;

public sealed class ValidationResult<TValue> : Result<TValue>, IValidationResult
{

    private ValidationResult(Error[] errors) : base(default, false, IValidationResult.ValidationError, System.Net.HttpStatusCode.BadRequest) { Errors = errors; }
    public Error[] Errors { get; }
    public static ValidationResult<TValue> WriteErrors(Error[] errors) => new(errors);
}
