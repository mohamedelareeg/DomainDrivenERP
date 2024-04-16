using DomainDrivenERP.Domain.Shared.Results;
using FluentValidation;
using MediatR;

namespace DomainDrivenERP.Application.Behaviors;

public class ValidationPiplineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPiplineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validation Request
        if (!_validators.Any())
        {
            return await next();
        }
        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(
                failure.PropertyName,
                failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            // IF any Errors, return validation Result
            return CreateValidationResult<TResponse>(errors);

            // Also I Could Throw ValidationException Here
            // throw new ValidationException(errors.ToString());
        }

        // Otherwise Return Next();
        return await next();
    }
    private static TResult CreateValidationResult<TResult>(Error[] errors) where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WriteErrors(errors) as TResult) !;
        }
        object validationResult = typeof(ValidationResult<>)
             .GetGenericTypeDefinition()
             .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
             .GetMethod(nameof(ValidationResult.WriteErrors)) !
             .Invoke(null, new object?[] { errors }) !;
        return (TResult)validationResult;
    }
}
