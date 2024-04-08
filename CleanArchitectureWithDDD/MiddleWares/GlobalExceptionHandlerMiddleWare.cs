using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitectureWithDDD.MiddleWares;
internal class GlobalExceptionHandlerMiddleWare : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleWare> _logger;
    public GlobalExceptionHandlerMiddleWare(ILogger<GlobalExceptionHandlerMiddleWare> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            _logger.LogError(error, error.Message);
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            var responseModel = Result.InternalServerError(error.Message);
            // TODO:: cover all validation errors
             switch (error)
            {
                case UnauthorizedAccessException e:
                    responseModel = Result.Unauthorized(error.Message);
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ValidationException e:
                    responseModel = Result.ValidationError(error.Message);
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case KeyNotFoundException e:
                    responseModel = Result.NotFound(error.Message);
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case DbUpdateException e:
                    responseModel = Result.BadRequest(error.Message);
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case TimeoutException e:
                    responseModel = Result.Timeout(error.Message);
                    response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    break;
                case NotImplementedException e:
                    responseModel = Result.NotImplemented(error.Message);
                    response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    break;
                case HttpRequestException e:
                    responseModel = Result.ServiceUnavailable(error.Message);
                    response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    break;
                case OperationCanceledException e:
                    responseModel = Result.ServiceUnavailable(error.Message);
                    response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    break;
                case FileNotFoundException e:
                    responseModel = Result.NotFound(error.Message);
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case DirectoryNotFoundException e:
                    responseModel = Result.NotFound(error.Message);
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case NotSupportedException e:
                    responseModel = Result.BadRequest(error.Message);
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case OutOfMemoryException e:
                    responseModel = Result.InternalServerError(error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                case StackOverflowException e:
                    responseModel = Result.InternalServerError(error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                case ArithmeticException e:
                    responseModel = Result.InternalServerError(error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                case FormatException e:
                    responseModel = Result.BadRequest(error.Message);
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    // unhandled error
                    responseModel = Result.InternalServerError(error.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var resultObj = new BaseResponse<Result>(responseModel.Error, new List<string> { responseModel.Error.Message }, responseModel.StatusCode, succeeded: false);
            string result = JsonSerializer.Serialize(resultObj);
            await response.WriteAsync(result);

        }
    }
}
