using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Interfaces;
using task_mananger_api.DTOs;

namespace task_mananger_api.Domain.UseCases;

public class CreateTask(ITaskRepository taskRepository)
{
    public TaskEntity Execute(CreateTaskDto payload)
    {
        var task = TaskEntity.FromPayload(payload);

        return taskRepository.Create(task);
    }
}