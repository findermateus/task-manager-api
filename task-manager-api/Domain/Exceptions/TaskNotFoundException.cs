namespace task_mananger_api.Domain.Exceptions;

public class TaskNotFoundException(int id)
    : DomainException($"Task with id {id} not found.", "TASK_NOT_FOUND", StatusCodes.Status404NotFound);
