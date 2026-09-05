using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Exceptions;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class GetTaskById(ITaskRepository taskRepository)
{
    public TaskEntity Execute(int id)
    {
        return taskRepository.GetById(id) ?? throw new TaskNotFoundException(id);
    }
}