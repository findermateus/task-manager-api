using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using task_mananger_api.Domain.Exceptions;

namespace task_mananger_api.Infrastructure.ExceptionHandling;

public class DomainExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        if (exception is DomainException domainException)
        {
            context.Result = new ObjectResult(new
            {
                message = domainException.Message,
                errorCode = domainException.ErrorCode,
            })
            {
                StatusCode = domainException.StatusCode
            };
        }
    }
}