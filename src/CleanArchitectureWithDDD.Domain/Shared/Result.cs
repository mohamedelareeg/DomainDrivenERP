using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Shared
{
    public class Result 
    {
        protected internal Result(bool isSuccess , Error error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException();
            if(!isSuccess && error == Error.None)
                throw new InvalidOperationException();
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true, Error.None);
        public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value , true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result Created() => new(true, Error.None);
        public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
        public static Result NotFound(string message = "The requested resource was not found.") =>
            new(false, new Error("NotFound", message));

        public static Result Unauthorized(string message = "Unauthorized access.") =>
            new(false, new Error("Unauthorized", message));

        public static Result Forbidden(string message = "Access to the requested resource is forbidden.") =>
            new(false, new Error("Forbidden", message));

        public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);

        public static Result<TValue> Failure<TValue>(string code, string message) =>
            new Result<TValue>(default, false, new Error(code, message));

        public static Result ValidationError(string message) =>
            new(false, new Error("ValidationError", message));

        public static Result<TValue> ValidationError<TValue>(string message) =>
            new Result<TValue>(default, false, new Error("ValidationError", message));

        public static Result<TValue> NotFound<TValue>(string message = "The requested resource was not found.") =>
            new Result<TValue>(default, false, new Error("NotFound", message));

        public static Result NoContent() => new(true, Error.None);

        public static Result Conflict(string message = "Conflict occurred while processing the request.") =>
            new(false, new Error("Conflict", message));

        public static Result BadRequest(string message = "The request is invalid.") =>
            new(false, new Error("BadRequest", message));

        public static Result<TValue> BadRequest<TValue>(string message = "The request is invalid.") =>
            new Result<TValue>(default, false, new Error("BadRequest", message));

        public static Result InternalServerError(string message = "An internal server error occurred.") =>
            new(false, new Error("InternalServerError", message));

        public static Result<TValue> InternalServerError<TValue>(string message = "An internal server error occurred.") =>
            new Result<TValue>(default, false, new Error("InternalServerError", message));

        public static Result ServiceUnavailable(string message = "The service is temporarily unavailable.") =>
            new(false, new Error("ServiceUnavailable", message));

        public static Result<TValue> ServiceUnavailable<TValue>(string message = "The service is temporarily unavailable.") =>
            new Result<TValue>(default, false, new Error("ServiceUnavailable", message));

        public static Result<TResult> Map<TValue, TResult>(Result<TValue> result, Func<TValue, TResult> mapper)
        {
            if (result.IsSuccess)
                return Result.Success(mapper(result.Value));
            return Result.Failure<TResult>(result.Error);
        }

        public static async Task<Result> TryAsync(Func<Task> action)
        {
            try
            {
                await action();
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Log the exception
                return Result.Failure(new Error("UnexpectedError", ex.Message));
            }
        }

        public static async Task<Result<TValue>> TryAsync<TValue>(Func<Task<TValue>> action)
        {
            try
            {
                var value = await action();
                return Result.Success(value);
            }
            catch (Exception ex)
            {
                // Log the exception
                return Result.Failure<TValue>(new Error("UnexpectedError", ex.Message));
            }
        }
        public static Result<TValue> Conflict<TValue>(string message = "Conflict occurred while processing the request.") =>
            new Result<TValue>(default, false, new Error("Conflict", message));

        public static Result Accepted(string message = "The request has been accepted for processing.") =>
            new(true, new Error("Accepted", message));

        public static Result<TValue> Accepted<TValue>(TValue value, string message = "The request has been accepted for processing.") =>
            new Result<TValue>(value, false, new Error("Accepted", message));

        public static Result<TValue> PartialContent<TValue>(TValue value, string message = "Partial content is returned.") =>
            new Result<TValue>(value, false, new Error("PartialContent", message));

        public static Result PartialContent(string message = "Partial content is returned.") =>
            new(true, new Error("PartialContent", message));

        public static Result<TValue> BadRequest<TValue>(Error error) => new Result<TValue>(default, false, error);

        public static Result BadRequest(Error error) => new Result(false, error);

        public static Result Unauthorized(Error error) => new Result(false, error);

        public static Result Unauthorized() => new Result(false, new Error("Unauthorized", "Unauthorized access."));

        public static Result Forbidden(Error error) => new Result(false, error);

        public static Result Forbidden() => new Result(false, new Error("Forbidden", "Access to the requested resource is forbidden."));

        public static Result<TValue> UnprocessableEntity<TValue>(string message = "The request is semantically incorrect and cannot be processed.") =>
            new Result<TValue>(default, false, new Error("UnprocessableEntity", message));

        public static Result UnprocessableEntity(string message = "The request is semantically incorrect and cannot be processed.") =>
            new Result(false, new Error("UnprocessableEntity", message));

        public static Result<TValue> Conflict<TValue>(Error error) => new Result<TValue>(default, false, error);

        public static Result Conflict(Error error) => new Result(false, error);

        public static Result Gone(string message = "The requested resource is no longer available.") =>
            new Result(false, new Error("Gone", message));

        public static Result<TValue> Gone<TValue>(string message = "The requested resource is no longer available.") =>
            new Result<TValue>(default, false, new Error("Gone", message));

        public static Result NotImplemented(string message = "The requested functionality is not implemented.") =>
            new Result(false, new Error("NotImplemented", message));

        public static Result<TValue> NotImplemented<TValue>(string message = "The requested functionality is not implemented.") =>
            new Result<TValue>(default, false, new Error("NotImplemented", message));

        public static Result MethodNotAllowed(string message = "The HTTP method used is not supported for this resource.") =>
            new Result(false, new Error("MethodNotAllowed", message));

        public static Result<TValue> MethodNotAllowed<TValue>(string message = "The HTTP method used is not supported for this resource.") =>
            new Result<TValue>(default, false, new Error("MethodNotAllowed", message));

        public static Result<TValue> FromResult<TValue>(System.Threading.Tasks.Task<TValue> task)
        {
            if (task.IsFaulted)
            {
                // Handle errors from the task
                return Result.Failure<TValue>(new Error("TaskError", task.Exception?.Message ?? "An error occurred."));
            }
            else if (task.IsCanceled)
            {
                return Result.Failure<TValue>(new Error("TaskCanceled", "The task was canceled."));
            }
            else
            {
                return Result.Success(task.Result);
            }
        }
        public static Result NoContent(string message = "No content to return.") =>
            new Result(true, new Error("NoContent", message));

        public static Result<TValue> NoContent<TValue>(string message = "No content to return.") =>
            new Result<TValue>(default, false, new Error("NoContent", message));

        public static Result<TValue> FromOptional<TValue>(TValue value, string errorMessage = "Value is null.") where TValue : class =>
            value != null ? Result.Success(value) : Result.Failure<TValue>(new Error("ValueIsNull", errorMessage));

        public static async Task<Result<TValue>> FromOptionalAsync<TValue>(Task<TValue> task, string errorMessage = "Value is null.") where TValue : class
        {
            var value = await task;
            return FromOptional(value, errorMessage);
        }

        public static async Task<Result<TValue>> FromResultAsync<TValue>(Task<Result<TValue>> task) =>
            await task;

        public static Result<TValue> Combine<TValue>(IEnumerable<Result> results, TValue value)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return Result.Failure<TValue>(result.Error);
            }
            return Result.Success(value);
        }

        public static async Task<Result<TValue>> CombineAsync<TValue>(IEnumerable<Task<Result>> tasks, TValue value)
        {
            var results = await Task.WhenAll(tasks);
            return Combine(results, value);
        }
        public static Result<TValue> Retry<TValue>(Func<Result<TValue>> action, int maxAttempts = 3)
        {
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                var result = action();
                if (result.IsSuccess)
                    return result;
            }
            return Result.Failure<TValue>(new Error("RetryFailed", "Maximum number of retry attempts reached."));
        }

        public static async Task<Result<TValue>> RetryAsync<TValue>(Func<Task<Result<TValue>>> action, int maxAttempts = 3)
        {
            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                var result = await action();
                if (result.IsSuccess)
                    return result;
            }
            return Result.Failure<TValue>(new Error("RetryFailed", "Maximum number of retry attempts reached."));
        }

        public static Result<TValue> Combine<TValue>(params Result<TValue>[] results)
        {
            foreach (var result in results)
            {
                if (result.IsFailure)
                    return Result.Failure<TValue>(result.Error);
            }
            return Result.Success(results.Last().Value);
        }

        public static async Task<Result<TValue>> CombineAsync<TValue>(params Task<Result<TValue>>[] tasks)
        {
            var results = await Task.WhenAll(tasks);
            return Combine(results);
        }


        public static Result<TValue> Timeout<TValue>(string message = "The operation timed out.") =>
            new Result<TValue>(default, false, new Error("Timeout", message));

        public static Result Timeout(string message = "The operation timed out.") =>
            new Result(false, new Error("Timeout", message));

    }

}
