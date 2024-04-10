using System.Net;

namespace CleanArchitectureWithDDD.Domain.Shared.Results;

public class Result
{
    protected internal Result(bool isSuccess, Error error, HttpStatusCode statusCode)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error { get; }

    public HttpStatusCode StatusCode { get; }

    public static Result Success() => new(true, Error.None, HttpStatusCode.OK);

    public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None, HttpStatusCode.OK);

    public static Result Failure(Error error) => new(false, error, HttpStatusCode.BadRequest);

    public static Result Created() => new(true, Error.None, HttpStatusCode.Created);

    public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result NotFound(string message = "The requested resource was not found.")
    {
        return new(false, new Error("NotFound", message), HttpStatusCode.NotFound);
    }

    public static Result Unauthorized(string message = "Unauthorized access.")
    {
        return new(false, new Error("Unauthorized", message), HttpStatusCode.Unauthorized);
    }

    public static Result Forbidden(string message = "Access to the requested resource is forbidden.")
    {
        return new(false, new Error("Forbidden", message), HttpStatusCode.Forbidden);
    }

    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error, HttpStatusCode.BadRequest);

    public static Result<TValue> Failure<TValue>(string code, string message) =>
        new Result<TValue>(default, false, new Error(code, message), HttpStatusCode.BadRequest);

    public static Result ValidationError(string message) =>
        new(false, new Error("ValidationError", message), HttpStatusCode.BadRequest);

    public static Result<TValue> ValidationError<TValue>(string message) =>
        new Result<TValue>(default, false, new Error("ValidationError", message), HttpStatusCode.BadRequest);

    public static Result<TValue> NotFound<TValue>(string message = "The requested resource was not found.") =>
        new Result<TValue>(default, false, new Error("NotFound", message), HttpStatusCode.NotFound);

    public static Result NoContent() => new(true, Error.None, HttpStatusCode.NoContent);

    public static Result Conflict(string message = "Conflict occurred while processing the request.") =>
        new(false, new Error("Conflict", message), HttpStatusCode.Conflict);

    public static Result BadRequest(string message = "The request is invalid.") =>
        new(false, new Error("BadRequest", message), HttpStatusCode.BadRequest);

    public static Result<TValue> BadRequest<TValue>(string message = "The request is invalid.") =>
        new Result<TValue>(default, false, new Error("BadRequest", message), HttpStatusCode.BadRequest);

    public static Result InternalServerError(string message = "An internal server error occurred.") =>
        new(false, new Error("InternalServerError", message), HttpStatusCode.InternalServerError);

    public static Result<TValue> InternalServerError<TValue>(string message = "An internal server error occurred.") =>
        new Result<TValue>(default, false, new Error("InternalServerError", message), HttpStatusCode.InternalServerError);

    public static Result ServiceUnavailable(string message = "The service is temporarily unavailable.") =>
        new(false, new Error("ServiceUnavailable", message), HttpStatusCode.ServiceUnavailable);

    public static Result<TValue> ServiceUnavailable<TValue>(string message = "The service is temporarily unavailable.") =>
        new Result<TValue>(default, false, new Error("ServiceUnavailable", message), HttpStatusCode.ServiceUnavailable);

    public static Result<TResult> Map<TValue, TResult>(Result<TValue> result, Func<TValue, TResult> mapper)
    {
        return result.IsSuccess ? Success(mapper(result.Value)) : Failure<TResult>(result.Error);
    }

    public static async Task<Result> TryAsync(Func<Task> action)
    {
        try
        {
            await action();
            return Success();
        }
        catch (Exception ex)
        {
            // Log the exception
            return Failure(new Error("UnexpectedError", ex.Message));
        }
    }

    public static async Task<Result<TValue>> TryAsync<TValue>(Func<Task<TValue>> action)
    {
        try
        {
            TValue? value = await action();
            return Success(value);
        }
        catch (Exception ex)
        {
            // Log the exception
            return Failure<TValue>(new Error("UnexpectedError", ex.Message));
        }
    }

    public static Result<TValue> Conflict<TValue>(string message = "Conflict occurred while processing the request.") =>
        new Result<TValue>(default, false, new Error("Conflict", message), HttpStatusCode.Conflict);

    public static Result Accepted(string message = "The request has been accepted for processing.") =>
        new(true, new Error("Accepted", message), HttpStatusCode.Accepted);

    public static Result<TValue> Accepted<TValue>(TValue value, string message = "The request has been accepted for processing.") =>
        new Result<TValue>(value, false, new Error("Accepted", message), HttpStatusCode.Accepted);

    public static Result<TValue> PartialContent<TValue>(TValue value, string message = "Partial content is returned.") =>
        new Result<TValue>(value, false, new Error("PartialContent", message), HttpStatusCode.PartialContent);

    public static Result PartialContent(string message = "Partial content is returned.") =>
        new(true, new Error("PartialContent", message), HttpStatusCode.PartialContent);

    public static Result<TValue> BadRequest<TValue>(Error error) => new Result<TValue>(default, false, error, HttpStatusCode.BadRequest);

    public static Result BadRequest(Error error) => new Result(false, error, HttpStatusCode.BadRequest);

    public static Result Unauthorized(Error error) => new Result(false, error, HttpStatusCode.Unauthorized);

    public static Result Unauthorized() => new Result(false, new Error("Unauthorized", "Unauthorized access."), HttpStatusCode.Unauthorized);

    public static Result Forbidden(Error error) => new Result(false, error, HttpStatusCode.Forbidden);

    public static Result Forbidden() => new Result(false, new Error("Forbidden", "Access to the requested resource is forbidden."), HttpStatusCode.Forbidden);

    public static Result<TValue> UnprocessableEntity<TValue>(string message = "The request is semantically incorrect and cannot be processed.") =>
        new Result<TValue>(default, false, new Error("UnprocessableEntity", message), HttpStatusCode.UnprocessableContent);

    public static Result UnprocessableEntity(string message = "The request is semantically incorrect and cannot be processed.") =>
        new Result(false, new Error("UnprocessableEntity", message), HttpStatusCode.UnprocessableContent);

    public static Result<TValue> Conflict<TValue>(Error error) => new Result<TValue>(default, false, error, HttpStatusCode.Conflict);

    public static Result Conflict(Error error) => new Result(false, error, HttpStatusCode.Conflict);

    public static Result Gone(string message = "The requested resource is no longer available.") =>
        new Result(false, new Error("Gone", message), HttpStatusCode.Gone);

    public static Result<TValue> Gone<TValue>(string message = "The requested resource is no longer available.") =>
        new Result<TValue>(default, false, new Error("Gone", message), HttpStatusCode.Gone);

    public static Result NotImplemented(string message = "The requested functionality is not implemented.") =>
        new Result(false, new Error("NotImplemented", message), HttpStatusCode.NotImplemented);

    public static Result<TValue> NotImplemented<TValue>(string message = "The requested functionality is not implemented.") =>
        new Result<TValue>(default, false, new Error("NotImplemented", message), HttpStatusCode.NotImplemented);

    public static Result MethodNotAllowed(string message = "The HTTP method used is not supported for this resource.") =>
        new Result(false, new Error("MethodNotAllowed", message), HttpStatusCode.MethodNotAllowed);

    public static Result<TValue> MethodNotAllowed<TValue>(string message = "The HTTP method used is not supported for this resource.") =>
        new Result<TValue>(default, false, new Error("MethodNotAllowed", message), HttpStatusCode.MethodNotAllowed);

    public static Result<TValue> FromResult<TValue>(Task<TValue> task)
    {
        if (task.IsFaulted)
        {
            // Handle errors from the task
            return Failure<TValue>(new Error("TaskError", task.Exception?.Message ?? "An error occurred."));
        }
        else
        {
            return task.IsCanceled ? Failure<TValue>(new Error("TaskCanceled", "The task was canceled.")) : Success(task.Result);
        }
    }

    public static Result NoContent(string message = "No content to return.") =>
        new Result(true, new Error("NoContent", message), HttpStatusCode.NoContent);

    public static Result<TValue> NoContent<TValue>(string message = "No content to return.") =>
        new Result<TValue>(default, false, new Error("NoContent", message), HttpStatusCode.NoContent);

    public static Result<TValue> FromOptional<TValue>(TValue value, string errorMessage = "Value is null.") where TValue : class =>
        value != null ? Success(value) : Failure<TValue>(new Error("ValueIsNull", errorMessage));

    public static async Task<Result<TValue>> FromOptionalAsync<TValue>(Task<TValue> task, string errorMessage = "Value is null.") where TValue : class
    {
        TValue value = await task;
        return FromOptional(value, errorMessage);
    }

    public static async Task<Result<TValue>> FromResultAsync<TValue>(Task<Result<TValue>> task) =>
        await task;

    public static Result<TValue> Combine<TValue>(IEnumerable<Result> results, TValue value)
    {
        foreach (Result result in results)
        {
            if (result.IsFailure)
            {
                return Failure<TValue>(result.Error);
            }
        }

        return Success(value);
    }

    public static async Task<Result<TValue>> CombineAsync<TValue>(IEnumerable<Task<Result>> tasks, TValue value)
    {
        Result[] results = await Task.WhenAll(tasks);
        return Combine(results, value);
    }

    public static Result<TValue> Retry<TValue>(Func<Result<TValue>> action, int maxAttempts = 3)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Result<TValue> result = action();
            if (result.IsSuccess)
            {
                return result;
            }
        }

        return Failure<TValue>(new Error("RetryFailed", "Maximum number of retry attempts reached."));
    }

    public static async Task<Result<TValue>> RetryAsync<TValue>(Func<Task<Result<TValue>>> action, int maxAttempts = 3)
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Result<TValue> result = await action();
            if (result.IsSuccess)
            {
                return result;
            }
        }

        return Failure<TValue>(new Error("RetryFailed", "Maximum number of retry attempts reached."));
    }

    public static Result<TValue> Combine<TValue>(params Result<TValue>[] results)
    {
        foreach (Result<TValue> result in results)
        {
            if (result.IsFailure)
            {
                return Failure<TValue>(result.Error);
            }
        }

        return Success(results.Last().Value);
    }

    public static async Task<Result<TValue>> CombineAsync<TValue>(params Task<Result<TValue>>[] tasks)
    {
        Result<TValue>[] results = await Task.WhenAll(tasks);
        return Combine(results);
    }

    public static Result<TValue> Timeout<TValue>(string message = "The operation timed out.") =>
        new Result<TValue>(default, false, new Error("Timeout", message), HttpStatusCode.RequestTimeout);

    public static Result Timeout(string message = "The operation timed out.") =>
        new Result(false, new Error("Timeout", message), HttpStatusCode.RequestTimeout);
}
