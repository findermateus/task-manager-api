using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class DeleteTask(ITaskRepository taskRepository)
{
    public TaskEntity? Execute(int id)
    {
        var task = taskRepository.GetById(id);

        if (task is null)
        {
            return null;
        }

        taskRepository.Delete(task);

        return task;
    }
}