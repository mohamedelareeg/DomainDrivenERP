using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Shared.Results;
public class BaseResponse<T>
{
    public BaseResponse()
    {

    }
    public BaseResponse(T data, HttpStatusCode statusCode, string message = null)
    {
        Succeeded = true;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        Data = data;
        StatusCode = statusCode;
    }
    public BaseResponse(HttpStatusCode statusCode, string message)
    {
        Succeeded = false;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        StatusCode = statusCode;
    }
    public BaseResponse(string message, HttpStatusCode statusCode, bool succeeded)
    {
        Succeeded = succeeded;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        StatusCode = statusCode;
    }
    public BaseResponse(string message, List<string> errors, HttpStatusCode statusCode, bool succeeded)
    {
        Succeeded = succeeded;
        Errors = errors;
        Message = message ?? (Succeeded ? "Success" : "Failure");
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; set; }
    public object Meta { get; set; }

    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
    public T Data { get; set; }
}
