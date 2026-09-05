namespace task_mananger_api.Domain.Exceptions;

public class InvalidTaskStateException(string message)
    : DomainException(message, "INVALID_TASK_STATE", StatusCodes.Status409Conflict);