namespace DomainDrivenERP.Domain.Shared.Results;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("ValidationError", "A Validation problem occurred.");
    Error[] Errors { get; }
}
