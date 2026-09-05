using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class CancelTask(ITaskRepository taskRepository)
{
    public TaskEntity? Execute(int taskId)
    {
        var task = taskRepository.GetById(taskId);

        if (task is null)
        {
            return null;
        }

        task.Cancel();

        taskRepository.Update(task);

        return task;
    }
}