using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Exceptions;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class CancelTask(ITaskRepository taskRepository)
{
    public TaskEntity Execute(int taskId)
    {
        var task = taskRepository.GetById(taskId) ?? throw new TaskNotFoundException(taskId);

        task.Cancel();

        taskRepository.Update(task);

        return task;
    }
}