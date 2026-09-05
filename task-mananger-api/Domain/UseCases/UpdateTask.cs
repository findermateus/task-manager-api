using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Exceptions;
using task_mananger_api.Domain.Interfaces;
using task_mananger_api.DTOs;

namespace task_mananger_api.Domain.UseCases;

public class UpdateTask(ITaskRepository taskRepository)
{
    public TaskEntity Execute(int id, UpdateTaskDto payload)
    {
        var task = taskRepository.GetById(id) ?? throw new TaskNotFoundException(id);

        task.Update(payload);

        taskRepository.Update(task);

        return task;
    }
}