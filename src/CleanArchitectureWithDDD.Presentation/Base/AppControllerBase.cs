using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureWithDDD.Presentation.Base;

[ApiController]
public class AppControllerBase : ControllerBase
{
    protected readonly ISender Sender;

    protected AppControllerBase(ISender sender)
    {
        Sender = sender;
    }
    protected IActionResult HandleFailure<T>(Result<T> result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult => BadRequest(
                CreateProblemDetails(
                    "Validation Error",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors)),
            _ => BadRequest(
                CreateProblemDetails("Bad Request",
                StatusCodes.Status400BadRequest,
                result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(
    string title,
    int status,
    Error error,
    Error[]? errors = null) => new()
    {
        Title = title,
        Status = status,
        Type = error.Code,
        Detail = error.Message,
        Extensions = { { nameof(errors), errors } }
    };

}
