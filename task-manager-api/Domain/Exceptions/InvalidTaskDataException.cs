namespace task_mananger_api.Domain.Exceptions;

public class InvalidTaskDataException(string message)
    : DomainException(message, "INVALID_TASK_DATA", StatusCodes.Status400BadRequest);