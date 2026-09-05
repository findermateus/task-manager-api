using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Exceptions;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class CompleteTask(ITaskRepository taskRepository)
{
    public TaskEntity Execute(int id)
    {
        var task = taskRepository.GetById(id) ?? throw new TaskNotFoundException(id);

        task.Complete();

        taskRepository.Update(task);

        return task;
    }
}