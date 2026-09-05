using task_mananger_api.Domain.Exceptions;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class DeleteTask(ITaskRepository taskRepository)
{
    public void Execute(int id)
    {
        var task = taskRepository.GetById(id) ?? throw new TaskNotFoundException(id);

        taskRepository.Delete(task);
    }
}