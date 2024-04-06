using System.Collections;
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
    public IActionResult CustomResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            if (typeof(IEnumerable<object>).IsAssignableFrom(typeof(T)))
            {
                int count = (result.Value as IEnumerable<object>)?.Count() ?? 0;
                var responseData = new { Count = count, Items = result.Value};
                return new OkObjectResult(new BaseResponse<object>(responseData, result.StatusCode));
            }
            else
            {
                return new OkObjectResult(new BaseResponse<T>(result.Value, result.StatusCode));
            }
        }
        else
        {
            if (result is IValidationResult validationResult)
            {
                var errorMessages = validationResult.Errors.Select(error => $"{error.Code} : {error.Message}").ToList();
                return new ObjectResult(new BaseResponse<T>(result.Error, errorMessages, result.StatusCode, succeeded: false))
                {
                    StatusCode = (int)result.StatusCode
                };

            }
            else
            {
                return new ObjectResult(new BaseResponse<T>(result.Error, new List<string> { result.Error.Message }, result.StatusCode, succeeded: false))
                {
                    StatusCode = (int)result.StatusCode
                };
            }
        }
    }


    #region HandleFailure
    //No need for this but i doesn't remove the code of it
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
    #endregion

}
