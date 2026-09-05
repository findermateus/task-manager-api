namespace task_mananger_api.Domain.Exceptions;

public abstract class DomainException(string message, string errorCode, int statusCode) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
    public string ErrorCode { get; } = errorCode;
}