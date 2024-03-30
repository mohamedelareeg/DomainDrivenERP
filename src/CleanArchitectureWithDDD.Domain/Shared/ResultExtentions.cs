using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Shared
{
    public static class ResultExtentions
    {
        //Ensures a specific condition is met, otherwise returns a failure result with the provided error.
        public static Result<T> Ensure<T>(this Result<T>result,Func<T,bool> predicate,Error error)//Extension Method
        {
            if(result.IsFailure)
            {
                return result;
            }
            if(predicate(result.Value))
            {
                return result;
            }
            return Result.Failure<T>(error);
        }
        public static Result<TOut> Map<TIn,TOut>(this Result<TIn> result,Func<TIn ,TOut> mappingFunc)
        {
            // If the result is successful, apply the mapping function to its value and return a new success result.
            // Otherwise, return a failure result with the original error.
            return result.IsSuccess ? Result.Success(mappingFunc(result.Value)) : Result.Failure<TOut>(result.Error);
        }
    }
}
